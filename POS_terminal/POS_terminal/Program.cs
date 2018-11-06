using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace POS_terminal
{
    class Program
    {
        //create a shopping cart to hold items to purchase 
        public static List<object> ShoppingCart = new List<object> { };
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static  DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        public static string fileName = Path.Combine(directory.FullName, "cactus_castle_data.csv");
        public static List<Products> fileContents = ReadCactusData(fileName);
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Prickly Peak!");
            Console.WriteLine("Your local Cactus Shoppe\n");
            MainMenu();
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<Products> ReadCactusData(string fileName)
        {
            List<Products> CactusData = new List<Products>();
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Products product = new Products();
                    string[] values = line.Split(',');

                    int parseInt;
                    if (int.TryParse(values[0], out parseInt))

                    {
                        product.ProductNumber = parseInt;
                    }

                    product.Name = values[1];

                    Category category;
                    if (Enum.TryParse(values[2], out category))
                    {
                        product.Category = category;
                    }
                        
                    product.Description = values[3];

                   
                    double parseDbl;
                    if (double.TryParse(values[4], out parseDbl))

                    {
                        product.Price = parseDbl;
                    }
                    CactusData.Add(product);
                }
            }
            return CactusData;
        }

        static void PrintMe(List<Products> ReadCactusData)
        {
            Console.WriteLine("{0,-5}{1,-40}{2,-20}", "","Item", "Price");
            foreach (Products item in ReadCactusData)
            {

                Console.WriteLine("{0,-5}{1,-40}${2,-20}", item.ProductNumber, item.Name, item.Price);
            }
        }

        static void MainMenu()
        {
            //present a menu to the user and allow them to choose an item by number or letter 

            var readFile = ReadCactusData(fileName);
            PrintMe(readFile);
          //print list of objects here 
            Console.WriteLine("\nWhat would you like to purchase? Choose a number: ");
            int itemID = int.Parse(Console.ReadLine());
            Console.WriteLine("How many would you like?");
            int quanity = int.Parse(Console.ReadLine());

            // select item from the list, make a copy of it, and add it to the empty shopping cart list 

            ContinuePurchase();
           
        }
        static void ContinuePurchase()
        {
            // allow client to redisplay the menu and to complete the purchase 
            Console.WriteLine("Would you like to buy another item? y/n");
            string continuePurchase = Console.ReadLine().ToLower();
            if (continuePurchase == "yes" || continuePurchase == "y")
            {
                MainMenu();

            }
            else if (continuePurchase == "no" || continuePurchase == "n")
            {
                CheckOut();
            }
            else
            {
                Console.WriteLine("Please type Yes/Y or No/N");
                ContinuePurchase();
            }
        }

        static void CheckOut()
        {
            //iterate through shopping cart to determine the subtotal and print the list to the screen 


           
            //display total including tax 


            GetPayment();

        }
        static void GrabProduct()
        {
            // Allow user to choose a quanity for the item ordered 
            // Add item to the shopping cart 
        }
        static void GetPayment()
        {
            //ask for payment type- cash, credit or check 
            Console.WriteLine("How would you like to pay?");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Credit");
            Console.WriteLine("3. Check");
            int paymentType = int.Parse(Console.ReadLine());
            if (paymentType == 1)
            {
                CashBack();
            }
            else if (paymentType == 2)
            {
                CreditCard();
            }
            else if (paymentType == 3)
            {
                Check();
            }
            else
            {
                Console.WriteLine("That was not a choice, please try again");
                GetPayment();
            }
        }

        static void CashBack()
        {
            //For cash, ask for amount tendered and provide change 


            DisplayReceipt();
        }

        static void Check()
        {
            //ask for routing and account number 


            DisplayReceipt();
        }

        static void CreditCard()
        {
            //get cc number, expiration date and cvv 


            DisplayReceipt();
        }

        static void DisplayReceipt()
        {
            //Display Receipt will all items ordered, subtotal, grand total, and payment info
            //build in security function to not show whole cc number or routing/acct numb

            Looper();
        }

        static void Looper()
        {
            Console.WriteLine("Would you like to do another transaction?");
            string looping = Console.ReadLine().ToLower();
            if (looping == "yes" || looping == "y")
            {
                // *******************clear the Shopping Cart*************
                ShoppingCart.Clear();
                MainMenu();
            }
            else if (looping == "no" || looping == "n")
            {
                Console.WriteLine("GoodBye");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please type Yes/Y or No/N");
                Looper();
            }
        }

    }
}
