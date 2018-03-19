using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

/* Owners of the Mongo Drivers: M. (n.d.). Mongocsharpdriver 2.5.0 (C. & R., Eds.). 
 * Retrieved March 13, 2018, from https://www.nuget.org/packages/mongocsharpdriver/2.5.0
 * 
 * 
 * */


namespace Trivia_Nation
{
    class Mongo
    {
        string connectionString { get; set; }
        MongoClient mClient { get; set; }
        MongoServer mServer { get; set; }
        MongoDatabase mDatabase { get; set; }
        MongoCollection questionCollection { get; set; }




        //creates a client that will be used to manipulate the server/question database
        public void createdDatabase(string connection)
        {
            // uses parameter to set the connectionString Property
            connectionString = connection;


            Console.WriteLine("Creating Client..........");
            //code boack that creates the  MongoClient that we will use for the editting of the database
            mClient = null;
            try
            {
                mClient = new MongoClient(connectionString);
                Console.WriteLine("Client Created Successfuly........");
                Console.WriteLine("Client: " + mClient.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Create Client.......");
                Console.WriteLine(ex.Message);
            }


            // this block of code sets up the server that the database will be located on
            mServer = null;
            try
            {
                Console.WriteLine("Getting Server object......");
                mServer = mClient.GetServer();
                Console.WriteLine("Server object created successfully....");
                Console.WriteLine("Server :" + mServer.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to getting Server Details");
                Console.WriteLine(ex.Message);
            }


            //createServer() must be ran before this code can be ran
            Console.WriteLine("Initiating Mongo Database.");
            MongoDatabase database = null;
            try
            {
                Console.WriteLine("Getting reference of database.......");
                database = mServer.GetDatabase("Triva Nation Question Bank");                          
                Console.WriteLine("Database Name : " + database.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Get reference of Database");
                Console.WriteLine("Error :" + ex.Message);
            }

            // this section of code creates a collection that question(documents) can be stored in
            questionCollection = null;
            try
            {
                questionCollection = database.GetCollection<Question>("Trivia Nation collection");
                Console.WriteLine(questionCollection.Count().ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to get collection from Database");
                Console.WriteLine("Error: " + ex.Message);
            }

            ObjectId oID = new ObjectId();


        }


        //below is junk code that will be used in the program *****DO NOT DELETE UNTIL PROGRAM IS COMPLETE


        /*
         *  Console.WriteLine("Inserting document to collection............");   
         try   
         {   
            Symbol symbol = new Symbol ();   
            symbol.Name = “Star”;   
            symbolcollection.Insert(symbol);   
            id = symbol.ID;   
  
            Symbol symbol = new Symbol ();   
            symbol.Name = “Star1”;   
            symbolcollection.Insert(symbol);   
            id = symbol.ID;   
  
            Console.WriteLine(symbolcollection.Count().ToString());   
         }   
         catch (Exception ex)   
         {   
            Console.WriteLine("Failed to insert into collection of Database " + database.Name);   
            Console.WriteLine("Error :" + ex.Message);   
         }   
  
         try   
         {   
            Console.WriteLine("Preparing Query Document............");   
            List< Symbol > query = symbolcollection.AsQueryable<Entity>().Where<Entity>(sb => sb.Name == "Kailash").ToList();   
  
            Symbol symbol = symbolcollection.AsQueryable<Entity>().Where<Entity>(sb => sb. ID == id).ToList();   
  
         }   
         catch (Exception ex)   
         {   
            Console.WriteLine("Failed to query from collection");   
            Console.WriteLine("Exception :" + ex.Message);   
         }   
         Console.WriteLine("");   
         Console.WriteLine("====================================================");   
         Console.ReadLine();   
      }   
   }   
   */


    }
}
