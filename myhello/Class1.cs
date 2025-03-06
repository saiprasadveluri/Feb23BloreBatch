using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class BankAccount
    {
        private string AccId;
        private string AccName;
        private double AccBalance;
        private double AccInterest;

        public BankAccount(string id, string name) : this(id, name, 1000, 5.5)
        {
        }

        public BankAccount(string id, string name, double balance, double interest) 
        {
            AccId = id;
            AccName = name;
            AccBalance = balance;
            AccInterest = interest;
        }

        public void Display()
        {
            Console.WriteLine("Account ID: " + AccId);
            Console.WriteLine("Account Name: " + AccName);
            Console.WriteLine("Account Balance: " + AccBalance);
        }

        public void calculateInterest()
        {
            double interest = AccBalance * AccInterest / 100;
            Console.WriteLine("Interest: " + interest);
        }
    }
    public class BankBranch
    {
        private string brname;
        private string location;
        private BankAccount[] accounts;
        BankManager manager;
        public BankBranch(string nm, string loc,string mgrName, string mgrContact)
        {
            brname = nm;
            location = loc;
            accounts = new BankAccount[5];
            manager = new BankManager(mgrName, mgrContact);
        }
        public void DisplayBranchData()
        {
            Console.WriteLine($"{brname} - {location}");
            
            manager.DispayData();
            //foreach (BankAccount b in accounts)
            //{
            //    b.Display();
            //}
            //manager.DispayData();
        }
    }
    public class BankManager
    {
        private string name;
        private string contactnumber;
        public BankManager(string nm, string cn)
        {
            name = nm;
            contactnumber = cn;
        }
        public void DispayData()
        {
            Console.WriteLine($"{name} - {contactnumber}");
        }
    }
}
