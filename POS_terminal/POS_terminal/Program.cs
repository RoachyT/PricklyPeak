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
        public static List<Products> ShoppingCart = new List<Products> { };
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static DirectoryInfo directory = new DirectoryInfo(currentDirectory);
        public static string fileName = Path.Combine(directory.FullName, "cactus_castle_data.csv");
        public static List<Products> fileContents = ReadCactusData(fileName);
        public static ValidatePayment val = new ValidatePayment();
        public static  decimal taxValue = 0.06m;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Prickly Peak!");
            Console.WriteLine("Your local Cactus Shoppe\n");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^");
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


                    decimal parseDbl;
                    if (decimal.TryParse(values[4], out parseDbl))

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
            Console.WriteLine("{0,-5}{1,-40}{2,-20}", "", "Item", "Price");
            Console.WriteLine("*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^*^");
            foreach (Products item in ReadCactusData)
            {

                Console.WriteLine("{0,-5}{1,-40}${2,-20}", item.ProductNumber, item.Name, item.Price);
            }
        }

        static void MainMenu()
        {
            //present a menu to the user and allow them to choose an item by number or letter 
            try
            {
            var readFile = ReadCactusData(fileName);
            PrintMe(readFile);

                // Allow user to choose a quanity for the item ordered 
                // Add item to the shopping cart
               
            Console.WriteLine("\nWhat would you like to purchase? Choose a number: ");
            int itemID = int.Parse(Console.ReadLine());
                if (itemID <= 12)
                {
                    Console.WriteLine("How many would you like?");
                    int quanity = int.Parse(Console.ReadLine());
                    // take itemID and use it to grab the whole Line and add it to shopping cart. Iterate as many times as the quanity.
                    if (quanity <= 50)
                    {
                    for (int i = 0; i < quanity; i++)
                    {
                        ShoppingCart.Add(readFile[itemID - 1]);
                    }
                    // select item from the list, make a copy of it, and add it to the empty shopping cart list 

                    ContinuePurchase();
                    }
                    else
                    {
                        Console.WriteLine("We only sell a max of 50 of each item. Try again.");
                        MainMenu();
                    }
                }
                else
                {
                    Console.WriteLine("Try again, pal.");
                    MainMenu();
                }
                
            }
            catch (FormatException)
            {

                Console.WriteLine("What?");
                MainMenu();
            }

        }

        static void ContinuePurchase()
        {
            // allow client to redisplay the menu and to complete the purchase 
            Console.WriteLine("Would you like to buy another item? y/n");
            string continuePurchase = Console.ReadLine().ToLower();
            var readFile = ReadCactusData(fileName);
            if (continuePurchase == "yes" || continuePurchase == "y")
            {
                MainMenu();

            }
            else if (continuePurchase == "no" || continuePurchase == "n")
            {
                CheckOut(ShoppingCart);
            }
            else
            {
                Console.WriteLine("Please type Yes/Y or No/N");
                ContinuePurchase();
            }
        }

        static void CheckOut(List<Products> ShoppingCart)
        {
            //iterate through shopping cart to determine the subtotal and print the list to the screen 
            Console.WriteLine("\nHere is your shopping cart: \n");
            foreach (Products x in ShoppingCart)
            {
                Console.WriteLine("{0,-40}${1,-20}", x.Name, x.Price);
            }
            decimal total = 0;
            foreach (Products y in ShoppingCart)
            {
                total += y.Price;

            }
            Console.WriteLine($"\nSubtotal: ${total}");
            decimal plusTax = (total * taxValue) + total;
            decimal finalTotal = Math.Round(plusTax, 2);

            //display total including tax 
            Console.WriteLine($"\nFinal Total: ${finalTotal}");

            GetPayment(finalTotal);

        }

        static void GetPayment(decimal finalTotal)
        {
            try
            {
                //ask for payment type- cash, credit or check 
                Console.WriteLine("How would you like to pay?");
                Console.WriteLine("1. Cash");
                Console.WriteLine("2. Credit");
                Console.WriteLine("3. Check");
                int paymentType = int.Parse(Console.ReadLine());
                if (paymentType == 1)
                {
                    CashBack(ShoppingCart, finalTotal);
                }
                else if (paymentType == 2)
                {
                    CreditCard(ShoppingCart, finalTotal);
                }
                else if (paymentType == 3)
                {
                    Check(ShoppingCart, finalTotal);
                }
                else
                {
                    Console.WriteLine("That was not a choice, please try again");
                    GetPayment(finalTotal);
                }
            }
            catch (FormatException)
            {

                Console.WriteLine("That was not a choice, please try again");
                GetPayment(finalTotal);
            }
        }
    
        static void CashBack(List<Products> ShoppingCart, decimal finalTotal)
        {
            //For cash, ask for amount tendered and provide change 
            Console.WriteLine($"Total Due: {finalTotal}");
            Console.WriteLine("What amount are you paying with?");
            decimal payWithAmount = decimal.Parse(Console.ReadLine());
            string paymentInfo = (payWithAmount - finalTotal).ToString();
            Console.WriteLine($"Change: {paymentInfo}");

            DisplayReceiptCash(ShoppingCart,finalTotal,payWithAmount, paymentInfo);
        }

        static void Check(List<Products> ShoppingCart, decimal finalTotal)
        {
            //ask for routing and account number 
            Console.WriteLine("Please enter your 9 digit routing number:");
            string routingNum = Console.ReadLine();
            if (val.ValidateRoutNum(routingNum))
            {
                Console.WriteLine("Enter your 10 digit account number: ");
                string acctNum = Console.ReadLine();
                if (val.ValidateAcctNum(acctNum))
                {
                    Console.WriteLine("Thank you!");
                    DisplayReceiptCheck(ShoppingCart, finalTotal, acctNum);
                }
                else
                {
                    Console.WriteLine("That wasn't the correct amount of digits, try again");
                    Check(ShoppingCart, finalTotal);
                }
            }
            else
            {
                Console.WriteLine("That wasn't the correct amount of digits, try again");
                Check(ShoppingCart, finalTotal);
            }  
        }

        static void CreditCard(List<Products> ShoppingCart, decimal finalTotal)
        {
                //get cc number, expiration date and cvv 
                Console.WriteLine("Please enter the 16 digit Credit Card Number");
                Console.WriteLine("example: xxxx-xxxx-xxxx-xxxx");
                string CreditNum = Console.ReadLine();
                if (val.ValidateCreditCard(CreditNum))
                {
                    Console.WriteLine("Please enter the Expiration Date XX/XX:");
                    string ExpDate = Console.ReadLine();
                    if (val.ValidateExpDate(ExpDate))
                    {
                        Console.WriteLine("Please Enter the 3 Digit CVV code:");
                        string CVVcode = Console.ReadLine();
                        if (val.ValidateCVV(CVVcode))
                        {
                            Console.WriteLine("Your transaction was approved!");
                            DisplayReceiptCredit(ShoppingCart, finalTotal, CreditNum);
                        }
                        else
                        {
                            Console.WriteLine("That wasn't the correct amount of digits, try again");
                            CreditCard(ShoppingCart, finalTotal);
                        }
                    }
                    else
                    {
                        Console.WriteLine("That wasn't the correct amount of digits, try again");
                        CreditCard(ShoppingCart, finalTotal);
                    }
                }
                else
                {
                    Console.WriteLine("That wasn't the correct amount of digits, try again");
                    CreditCard(ShoppingCart, finalTotal);
                }
        }

        static void DisplayReceiptCash(List<Products> ShoppingCart, decimal finalTotal,  decimal payWithAmount, string paymentInfo)
        {
            //Display Receipt will all items ordered, subtotal, grand total, and payment info
            //build in security function to not show whole cc number or routing/acct numb
            Console.WriteLine();
            Console.WriteLine("_______________________________________________");
            Console.WriteLine("Thank you for Shopping at the Prickly Peak!");
            Console.WriteLine("_______________________________________________");
            foreach (Products x in ShoppingCart)
            {
                Console.WriteLine("{0,-40}${1,-20}", x.Name, x.Price);
            }
            Console.WriteLine("_______________________________________________");
            Console.WriteLine($"Total:${finalTotal}");
            Console.WriteLine($"Payment method Cash: ${payWithAmount}");
            Console.WriteLine($"Change: ${paymentInfo}");
            Console.WriteLine("_______________________________________________\n");
            Looper();
        }
        static void DisplayReceiptCheck(List<Products> ShoppingCart, decimal finalTotal, string acctNum)
        {
            Console.WriteLine();
            Console.WriteLine("_______________________________________________");
            Console.WriteLine("Thank you for Shopping at the Prickly Peak!");
            Console.WriteLine("_______________________________________________");
            foreach (Products x in ShoppingCart)
            {
                Console.WriteLine("{0,-40}${1,-20}", x.Name, x.Price);
            }
            Console.WriteLine("_______________________________________________");
            Console.WriteLine($"Total:${finalTotal}");
            Console.WriteLine($"Payment method Check: ACCT XXXXX{acctNum.Substring(5)}");

            Console.WriteLine("_______________________________________________\n");
            Looper();
        }
        static void DisplayReceiptCredit(List<Products> ShoppingCart, decimal finalTotal, string CreditNum)
        {
            Console.WriteLine();
            Console.WriteLine("_______________________________________________");
            Console.WriteLine("Thank you for Shopping at the Prickly Peak!");
            foreach (Products x in ShoppingCart)
            {
                Console.WriteLine("{0,-40}${1,-20}", x.Name, x.Price);
            }
            Console.WriteLine("_______________________________________________");
            Console.WriteLine($"Total:${finalTotal}");
            Console.WriteLine($"Payment method Credit XXXX-XXXX-XXXX-{CreditNum.Substring(15)}");
            Console.WriteLine("_______________________________________________\n");
            Looper();
        }


        static void Looper()
        {
            Console.WriteLine("Would you like to do another transaction? y/n");
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
