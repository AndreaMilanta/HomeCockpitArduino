/**
 * Class that represents a Serial input
 * It manages all conversions from reading data to filling the FIFO with proper Messages
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Diagnostics;
using Cockpit.Source.DataManagement;
using Cockpit.Source.Exceptions;
using Cockpit.Source.Common;
using Cockpit.Source.DataManagement.Messages;
using Cockpit.Source.DataManagement.Messages.Values;

namespace Cockpit.Source.Input.Serial
{
    class Serial : Input
    {
        //Constants
        public static const string COM_PORT = "COM3";
        public static const int BAUDRATE = 115200;

        //MUST MODIFY THE EQUIVALENT CONSTANTS ON THE ARDUINO SIDE
        public static const char DIV_SYM = '&';
        public static const char INT_SYM = 'i';
        public static const char FLT_SYM = 'f';
        public static const char BOL_SYM = 'b';
        public static const char BTE_SIM = 't';
        public static const char DEC_DIV = '.';

        private readonly static TraceSource tracer = new TraceSource(Cnst.SERIAL_TRACESRC); 

        private SerialPort serial;

        //Constructor
        public Serial()
        {
            serial = getSerial();
            serial.DiscardInBuffer();
            serial.DataReceived += new SerialDataReceivedEventHandler(serialDataReceivedHandler);       //Attach event handler
        }

        //Handler for DataReceived event
        private void serialDataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadExisting();
            try
            {
                Message mex = interpretString(data);
                this.enqueue(mex);
            }
            catch(InvalidStringException)
            {
                //Log!!
            }
            catch(ChannelOutOfBoundException)
            {
                //Log!!
            }
            catch(Exception)
            {
                //Log!!
            }
        }

        //Creates and opens a serial connection
        static SerialPort getSerial()
        {
            SerialPort serial = null;
            string baudrate = BAUDRATE.ToString();
            string comName = COM_PORT;
            bool areWeDone = true;
            do
            {
                areWeDone = true;
                try
                {
                    serial = new SerialPort(COM_PORT, BAUDRATE);
                    serial.Open();
                }
                catch (Exception ex)
                {
                    Console.Write("Serial Connection to " + comName + " (" + baudrate + ")" + " failed");
                    //Case port already open within the project. Close the port and tries again
                    if (ex is InvalidOperationException)
                    {
                        Console.WriteLine(" (Port already open)");
                        serial.Close();
                        Console.WriteLine("Closing Port...");
                        Console.WriteLine("Trying Again...");
                    }
                    else
                    {
                        if (ex is UnauthorizedAccessException)
                            Console.Write(" (Port is in use by another process)");      //Tells user the port is in use by another process
                        Console.WriteLine();
                        Console.Write("Try again with the same port? Y/N/D(default) ");
                        string s = Console.ReadLine().ToLower();
                        //case default: set default com name
                        if (s.Equals("d"))
                        {
                            comName = COM_PORT;
                            Console.WriteLine("Trying Again with default setting...");
                        }
                        //case new port: user sets new port name
                        else if (!s.Equals("y"))
                        {
                            Console.Write("Com Port Name: ");
                            comName = Console.ReadLine().ToUpper();
                            Console.WriteLine("Trying Again...");
                        }
                    }
                    areWeDone = false;
                }
            } while (!areWeDone);
            Console.WriteLine("Successful Connection to " + comName);
            return serial;
        }

        //Creates a message from the string
        static Message interpretString(string data)
        {

            Channel chan = readChan(data);
            Value value = readValueAndType(data);
            switch (chan)y
            {
                case Channel.LIGHTS:
                    return new LightsMessage((ByteValue)value);
                default:
                    throw new ChannelOutOfBoundException();
            }
        }

        //Reads the Channel from the string: s is modified!!
        //Throws ChannelOutOfBoundException and InvalidStringException
        private static Channel readChan(string s)
        {
            int chan;
            int first = s.IndexOf(DIV_SYM);             //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &    
            first = s.IndexOf(DIV_SYM);

            /*Reads channel as ASCII of first char after & */
            char chanStr = s.ElementAt(0);

            /*  Reads channel as decimal string
            string chanStr;  
            chanStr = s.Substring(0, first);
             */ 
            s = s.Substring(first, length - first);     //Eliminates the channel from s
            try
            {
                chan = Convert.ToInt32(chanStr);
                if(chan >= 0 || chan < (int)Channel.LAST)
                    return (Channel)chan;
                throw new ChannelOutOfBoundException();

            }
            catch (Exception)
            {
                throw new InvalidStringException();
            }
        }

        //reads the type of value contained. s is modified!!
        //Return information is the Classtype rather than the class value itself
        private static Value readValueAndType(string s)
        {
            int first = s.IndexOf(DIV_SYM);          //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &    
            if (s.Length == 0)
                throw new InvalidStringException();
            switch (s.ElementAt(0))            //Switch on the first char after &
            {
                case INT_SYM:
                    return readValue(s, new IntValue(0));
                case FLT_SYM:
                    try
                    {
                        int decLeng = Convert.ToInt16(s.ElementAt(1));          //Reads char value (ASCII CODE) of the float decimal length
                        return readValue(s, new FloatValue(0, decLeng));    
                    } 
                    catch(Exception)
                    {
                        throw new InvalidStringException();
                    }
                case BOL_SYM:
                    return readValue(s, new BoolValue(false));
                case BTE_SIM:
                    return readValue(s, new ByteValue(0));
                default: throw new InvalidStringException();
            }
        }

        //Reads an int value from string s
        private static Value readValue(string s, IntValue v)
        {
            int first = s.IndexOf(DIV_SYM);          //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &    
            s = s.Substring(0, s.IndexOf(DIV_SYM));
            if (s.Length == 0)
                throw new InvalidStringException();
            try
            {
                short value = Convert.ToInt16(s);
                return new IntValue(value);
            }
            catch(Exception)
            {
                throw new InvalidStringException();
            }
        }
         
        //Reads a float value from string s
        private static Value readValue(string s, FloatValue v)
        {
            int first = s.IndexOf(DIV_SYM);          //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &   
            s = s.Substring(0, s.IndexOf(DIV_SYM));
            if (s.Length == 0)
                throw new InvalidStringException();
            try
            {
                float value = Convert.ToInt16(s);
                return new FloatValue(value);
            }
            catch(FormatException)
            {
                throw new InvalidStringException();
            }
            catch(OverflowException)
            {
                throw new InvalidStringException();
            }
        }

        //Reads a byte value from string s
        private static Value readValue(string s, ByteValue v)
        {
            int first = s.IndexOf(DIV_SYM);          //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &    
            s = s.Substring(0, s.IndexOf(DIV_SYM));
            if (s.Length == 0)
                throw new InvalidStringException();
            try
            {
                byte value = Convert.ToByte(s);
                return new ByteValue(value);
            }
            catch(Exception)
            {
                throw new InvalidStringException();
            }
        }

        //Reads a boolean value from string s
        private static Value readValue(string s, BoolValue v)
        {
            int first = s.IndexOf(DIV_SYM);          //Reads up to the the first &
            int length = s.Length;
            s = s.Substring(++first, length - first);   //Eliminates first &    
            s = s.Substring(0, s.IndexOf(DIV_SYM));
            try
            {
                byte value = Convert.ToByte(s);
                if(value == 0)
                    return new BoolValue(false);
                if(value == 1)
                    return new BoolValue(true);
                throw new InvalidStringException();
            }
            catch(Exception)
            {
                throw new InvalidStringException();
            }
        }
    }
}
