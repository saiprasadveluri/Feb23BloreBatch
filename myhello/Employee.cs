using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
            Age =25;
        }

        public void  SetAge(int age)
        {
            
            if(Age < 0)
            {
               throw new AgeException("Age cant be negetive");
            }
            Age = age;
        }
        public void display()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}");
        }
    }

    class BankAccounts
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public double Balance { get; set; }
        public BankAccounts(int accountNumber, string accountHolderName,double balance)
        {
            AccountNumber = accountNumber;
            AccountHolderName = accountHolderName;
            Balance = balance;
        }
        public void Deposit(double amount)
        {
            Balance += amount;
        }
        public void Withdraw(double amount)
        {
            if (Balance < amount)
            {
                throw new InsufficientBalanceException("Insufficient balance");
            }
            Balance -= amount;
        }
        public void Display()
        {
            Console.WriteLine($"Account Number: {AccountNumber}, Account Holder Name: {AccountHolderName}, Balance: {Balance}");
        }
    }
}
