using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{ 
    
    public abstract class Card
    {
        private double turnoverPrevMonth;
        public string Owner { get; set; }
        public double Turnover 
        {
            get { return turnoverPrevMonth; }
            set
            {
                if(value < 0)
                {
                    throw new Exception("The turnover for the previous month can not be a negative value!");
                }
                else
                {
                    turnoverPrevMonth = value;
                }
            } 
        }
        public double InitialDiscountRate { get; set; }

        public Card(string owner, double turnover = 0, double initialRate = 0)
        {
            Owner = owner;
            try
            {
                Turnover = turnover;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            InitialDiscountRate = initialRate;
        }

        //Each subclass implements it on its own depending on the initial discount rate and the turnover for the previous month
        public abstract double DiscountRate();
       
    }

    public class BronzeCard : Card
    {
        public BronzeCard(string owner, double turnover = 0) : base(owner, turnover) {}

        public override double DiscountRate()
        {
            double discountRate = InitialDiscountRate;

            if (Turnover >= 100 && Turnover <= 300)
                discountRate = 1;

            if(Turnover > 300)
                discountRate = 2.5;

            return discountRate;
        }
    }
    public class SilverCard : Card
    {
        public SilverCard(string owner, double turnover = 0) : base(owner, turnover) 
        {
            InitialDiscountRate = 2;
        }

        public override double DiscountRate()
        {
            double discountRate = InitialDiscountRate;

            if (Turnover > 300)
                discountRate = 3.5;

            return discountRate;
        }
    }
    public class GoldCard : Card
    {
        public GoldCard(string owner, double turnover = 0) : base(owner, turnover)
        {
            InitialDiscountRate = 2;
        }

        public override double DiscountRate()
        {
            double discountRate = InitialDiscountRate;

            int growth = ((int)Turnover) / 100;

            //The discount rate grows 1% for each $100 from the turnover, capping at 10%.
            discountRate += growth;

            if (discountRate > 10.0)
                discountRate = 10.0;

            return discountRate;
        }
    }

    public static class PurchaseInfo
    {
        public static void OutputPurchaseInfo(Card card, double purchaseValue)
        {
            //polymorphic behaviour
            double discRate = card.DiscountRate();

            if (purchaseValue <= 0)
            {
                throw new Exception("The purchase value must be a positive number.");
            }
            else
            {
                double discount = (discRate/100.00) * purchaseValue;
                double totalPurchaseValue = purchaseValue - discount;

                Console.WriteLine("Purchase value: ${0:0.00}", purchaseValue);
                Console.WriteLine("Discount rate: {0:0.0}%", discRate);
                Console.WriteLine("Discount: ${0:0.00}", discount);
                Console.WriteLine("Total: ${0:0.00}", totalPurchaseValue);
                Console.WriteLine();
            }
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Card card1 = new BronzeCard("John Smith",0);
            try
            {
                PurchaseInfo.OutputPurchaseInfo(card1, 150);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Card card2 = new SilverCard("Ivan Ivanov", 600);
            try
            {
                PurchaseInfo.OutputPurchaseInfo(card2, 850);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Card card3 = new GoldCard("Mary Jones", 1500);
            try
            {
                PurchaseInfo.OutputPurchaseInfo(card3, 1300);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
