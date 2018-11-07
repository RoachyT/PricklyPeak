using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace POS_terminal
{
    class ValidatePayment
    {
        //validate credit card digits 
        public string CreditCard { get; set; }
        public string ExpDate { get; set; }
        public string CVVCode { get; set; }
        //validate routing number 
        public string RoutingNum { get; set; }
        //validate account number 
        public string AcctNum { get; set; }

        public ValidatePayment()
        {
        }
        public ValidatePayment(string creditCard, string expDate, string cvvCode, string routingNum, string acctNum)
        {
            CreditCard = creditCard;
            ExpDate = expDate;
            CVVCode = cvvCode;
            RoutingNum = routingNum;
            AcctNum = acctNum;
        }



        public bool ValidateCreditCard(string creditCard)
        {
            if (Regex.IsMatch(creditCard, @"([0-9]{4}\-[0-9]{4}\-[0-9]{4}\-[0-9]{4})"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateExpDate(string expDate)
        {
            if (Regex.IsMatch(expDate, @"([0-1]{1}[0-9]{1}\/[1-2]{1}[0-9]{1})"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateCVV(string cvvCode)
        {
            if (Regex.IsMatch(cvvCode, @"([0-9]{3})"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateAcctNum(string acctNum)
        {
            if (Regex.IsMatch(acctNum, @"([0-9]{9})"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateRoutNum(string routingNum)
        {
            if (Regex.IsMatch(routingNum, @"([0-9]{10})"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
