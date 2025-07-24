using System;
using System.IO;

namespace PolymorphismDemo
{
    // Interface defining the contract for quittable objects
    public interface IQuittable
    {
        void Quit();
    }

    // Employee class implementing IQuittable interface with file-based logging
    public class Employee : IQuittable
    {
        // Private fields for encapsulation
        private int _id;
        private string _firstName;
        private string _lastName;
        private DateTime _hireDate;

        // Properties with validation
        public int Id
        {
            get => _id;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Employee ID must be positive.");
                _id = value;
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("First name cannot be empty.");
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Last name cannot be empty.");
                _lastName = value;
            }
        }

        public DateTime HireDate
        {
            get => _hireDate;
            set => _hireDate = value;
        }

        // Constructor initializing employee data
        public Employee(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            HireDate = DateTime.Now;
        }

        // Implementation of IQuittable interface
        public void Quit()
        {
            // Calculate employment duration
            TimeSpan employmentDuration = DateTime.Now - HireDate;

            // Create resignation log entry
            string logEntry = $"Resignation Log - {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n" +
                            $"Employee ID: {Id}\n" +
                            $"Name: {FirstName} {LastName}\n" +
                            $"Employment Duration: {employmentDuration.Days} days\n" +
                            $"-----------------------------------\n";

            // Log to file (in the same directory as the executable)
            string logPath = "resignation_log.txt";
            File.AppendAllText(logPath, logEntry);

            Console.WriteLine($"Employee {FirstName} {LastName} has resigned. Details logged to {logPath}");
        }

        // Override ToString for better object representation
        public override string ToString()
        {
            return $"Employee(ID: {Id}, Name: {FirstName} {LastName}, Hired: {HireDate:d})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Employee Resignation System Demo\n");

                // Create an employee
                Employee employee = new Employee(1, "John", "Doe");
                Console.WriteLine($"Created: {employee}\n");

                // Demonstrate polymorphism by using interface type
                IQuittable quittableEmployee = employee;
                
                // Simulate some time passing
                System.Threading.Thread.Sleep(2000);

                // Call Quit through the interface reference
                Console.WriteLine("Processing resignation...");
                quittableEmployee.Quit();

                Console.WriteLine("\nCheck resignation_log.txt for detailed log entry.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}