using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCrudeOilToTweet
{
    class Program
    {

        static string last = "";
        static string lastT = "";
        static string lastlast = "";
        static string lastlastT = "";

        static void Main(string[] args)
        {
            Console.WriteLine("he");
            Console.WriteLine(GetOIL().Result);
            Console.ReadLine();
        }

        public static async Task<string> GetOIL(string subject = "", string body = "")
        {
            try
            {
                string current = subject + body;
                if (true) //time is right to try get.
                { //see https://www.eia.gov/petroleum/supply/weekly/
                    string uri = "http://ir.eia.gov/wpsr/table4.csv";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                    using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                    using (Stream stream = response.GetResponseStream())
                    {
                        /*using (StreamReader reader = new StreamReader(stream))
                        {
                            string s = await reader.ReadToEndAsync();
                            lastlastT = lastT; lastT = current;


                            return s;
                        }*/
                        using (TextFieldParser parser = new TextFieldParser(stream))
                        //  using (TextFieldParser parser = new TextFieldParser(@"c:\temp\OIL.txt"))
                        {
                            string s = "";
                            parser.TextFieldType = FieldType.Delimited;
                            parser.SetDelimiters(",");

                            while (!parser.EndOfData)
                            {
                                //Process row
                                string[] fields = parser.ReadFields();

                                switch (fields[0])
                                {
                                    case "STUB_1":
                                        break;

                                    case "Crude Oil":
                                        s += "Crude Oil: " + fields[3] + "M\r\n";
                                        break;
                                    case "Total Motor Gasoline":
                                        s += "Gasoline: " + fields[3] + "M\r\n";
                                        break;
                                    case "Distillate Fuel Oil":
                                        s += "Distillate: " + fields[3] + "M\r\n";
                                        break;
                                    case "Cushing":
                                        s += "Cushing: " + fields[3] + "M\r\n";
                                        break;
                                    case "Total Stocks (Including SPR)":
                                        s += "Total: " + fields[3] + "M\r\n";
                                        break;

                                    default:
                                        break;
                                }

                            }
                            return s;
                        }
                    }
                    /*using (TextFieldParser parser = new TextFieldParser(@"c:\temp\OIL.txt"))
                    {
                        string s = "";
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");

                        while (!parser.EndOfData)
                        {
                            //Process row
                            string[] fields = parser.ReadFields();

                            switch (fields[0])
                            {
                                case "STUB_1":
                                    break;

                                case "Crude Oil":
                                    s += "Crude Oil: " + fields[3] + "M\r\n";
                                    break;
                                case "Total Motor Gasoline":
                                    s += "Gasoline: " + fields[3] + "M\r\n";
                                    break;
                                case "Distillate Fuel Oil":
                                    s += "Distillate: " + fields[3] + "M\r\n";
                                    break;
                                case "15 ppm sulfur and Under":
                                    return s;

                                default:
                                    break;
                            }

                        }
                    }*/
                }
                return "";
            }



            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
