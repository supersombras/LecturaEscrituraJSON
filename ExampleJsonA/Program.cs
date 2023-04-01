using ExampleJsonA.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ExampleJsonA
{
    class Program 
    {
        private static string _path = @"\\Contacts.json";
        static void Main(string[] args)
        {
            //var contacts = GetContactsFromWeb();
            //SerializeJsonFile(contacts);
            //DeserializeJSonFile(contacts);
            var contacts = GetContactsJsonFromFile();
            //DeserializeJSonFile(contacts);
            ReadingJsonWithJsonTextReader(contacts);
        }

        #region  "writing Json"
        public static void SerializeJsonFile(List<Contact>contacts)
        {
            string contactsJson = JsonConvert.SerializeObject(contacts.ToArray(), Formatting.Indented); 
            File.WriteAllText(_path, contactsJson);
          }

        public static List<Contact> GetContactsFromWeb()
        {
            List<Contact> contacts = new List<Contact>()
            {
                new Contact
                {
                    Name="Bob uncle",
                    DateOfBirth= new DateTime(1970,01,12),
                            Address= new Address
                            {
                                Street="Fisher",
                                Number=12,
                                        City= new City
                                        {
                                            Name="Cox",
                                            Country= new Country
                                                    {
                                                        Code="Sp",
                                                           Name="Saint Port "
                                                    }
                                        }
                    } ,
                            Phones= new List<Phone>
                            {
                                new Phone{Name="Pers", Number="1223"},
                                new Phone{Name="Another", Number="143223"},
                            }
                },
                new Contact
                {
                    Name="Joe",
                    DateOfBirth= new DateTime(1970,01,12),
                            Address= new Address
                            {
                                Street="Builder",
                                Number=1212,
                                        City= new City
                                        {
                                            Name="Foxy",
                                            Country= new Country
                                                    {
                                                        Code="PR",
                                                           Name="Reader Pearl "
                                                    }
                                        }
                    } ,
                            Phones= new List<Phone>
                            {
                                new Phone{Name="One", Number="1223"},
                                new Phone{Name="Two", Number="143223"},
                            }
                }, 
                new Contact
                {
                    Name="Baby",
                    DateOfBirth= new DateTime(1970,01,12),
                            Address= new Address
                            {
                                Street="Peacemaker",
                                Number=232,
                                        City= new City
                                        {
                                            Name="Dinsaur",
                                            Country= new Country
                                                    {
                                                        Code="CD",
                                                           Name="Connection Date "
                                                    }
                                        }
                    } ,
                            Phones= new List<Phone>
                            {
                                new Phone{Name="Three", Number="1223"},
                                new Phone{Name="Four", Number="143223"},
                            }
                },
                 new Contact
                {
                    Name="Situation",
                    DateOfBirth= new DateTime(1970,01,12),
                            Address= new Address
                            {
                                Street="Finisher",
                                Number=236,
                                        City= new City
                                        {
                                            Name="Fantasy",
                                            Country= new Country
                                                    {
                                                        Code="EF",
                                                           Name="End of the film",
                                                    }
                                        }
                    } ,
                            Phones= new List<Phone>
                            {
                                new Phone{Name="Three", Number="7878"},
                                new Phone{Name="Four", Number="2349"},
                            }
                },
            };

            return contacts;
        }

        #endregion

        #region "Reading Json with model"
        public static string GetContactsJsonFromFile()
        {
            string contactsJsonFromFile;

            using(StreamReader reader= new StreamReader(_path))
            {
                contactsJsonFromFile = reader.ReadToEnd();
             }
            return contactsJsonFromFile;
        }

        public static void DeserializeJSonFile(string contactsJsonFromFile)
        {
            Console.WriteLine(contactsJsonFromFile);
            var contacts = JsonConvert.DeserializeObject<List<Contact>>(contactsJsonFromFile); //mapeamos los objetos del modelo
            Console.WriteLine(String.Format("Bob live  in this list is on: {0}{1}", 
                contacts[0].Address.Street, contacts[1].Address.Number));

            Console.WriteLine(string.Format("Bob´s date of birth is on:{0}", contacts[0].DateOfBirth.ToString()));
        }

        #endregion

        #region "Writting Json without model"
        public static void WritingJsonWithJsonTextWriter(string contactsJsonFromFile)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(contactsJsonFromFile));

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value:{1}", reader.TokenType, reader.Value);
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                }
            }
        }

        public static void ReadingJsonWithJsonTextReader(string contactsJsonFromFile)
        {
            
            JsonTextReader reader = new JsonTextReader(new StringReader(contactsJsonFromFile));

            string dateofBirth = string.Empty;
            while (reader.Read())
            {
                if (string.IsNullOrEmpty(dateofBirth))
                {
                    if(reader.Value!=null&& reader.TokenType == JsonToken.Date)
                    {
                        dateofBirth = DateTime.Parse(reader.Value.ToString()).ToShortDateString();
                    }
                }
            }    
            Console.WriteLine("This date is the birthday on: "+ dateofBirth);
        }

        #endregion
    }

}


