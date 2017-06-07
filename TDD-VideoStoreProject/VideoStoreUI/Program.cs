//using System;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VideoStoreBL;
using Console = Colorful.Console;

namespace VideoStoreUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new OurDateTime();
            var rentals = new VideoRentals(date);

            var VidStore = new VideoStore(rentals);
            Ui ui = new Ui( VidStore, rentals);
            ui.GenerateData();
            int R = 154;
            int G = 255;
            int B = 255;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteAscii("Video Store", Color.FromArgb(R, G, B));

                R -= 58;
                G -= 16;
                Thread.Sleep(300);
            }
            Thread.Sleep(2000);
            while (true)
            {
                Console.Clear();

                var menu = new string[] { "1. Add Movie",
                                     "2. List Customers",
                                     "3. Get Rentals For",
                                     "4. Rent Movie",
                                     "5. Return Movie"};
                var genres = new string[]
                {
                "1. Comedy",
                "2. Action",
                "3. Thriller",
                "4. Documentary",
                "5. SciFi"
                };
                int[] rgb = new int[] { 154,
                     255,
                     255};



                for (int i = 0; i < 5; i++)
                {
                    Console.WriteAscii(menu[i], Color.FromArgb(rgb[0], rgb[1], rgb[2]));

                    rgb[0] -= 38;
                    rgb[1] -= 19;
                }
                rgb = ResetColors(rgb);
                switch (Console.ReadLine())
                {
                    case "1":
                        string title;
                        MovieGenre genre;
                        Movie newMovie;
                        Console.Clear();
                        Console.WriteAscii("ENTER TITLE:", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        title = Console.ReadLine();
                        if (string.IsNullOrEmpty(title))
                        {
                            Console.WriteLine("Movie title can not be empty!", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                            Console.ReadLine();
                            break;
                        }
                        Console.Clear();
                        Console.WriteAscii("What Genre?", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        for (int i = 0; i < 5; i++)
                        {
                            Console.WriteAscii(genres[i], Color.FromArgb(rgb[0], rgb[1], rgb[2]));

                            rgb[0] -= 38;
                            rgb[1] -= 19;
                        }
                        rgb = ResetColors(rgb);
                        switch (Console.ReadLine())
                        {
                            case "1":
                                newMovie = new Movie() { Title = title, Genre = MovieGenre.Comedy };
                                ui.AddMovie(newMovie);
                                break;
                            case "2":
                                newMovie = new Movie() { Title = title, Genre = MovieGenre.Action };
                                ui.AddMovie(newMovie);
                                break;
                            case "3":
                                newMovie = new Movie() { Title = title, Genre = MovieGenre.Thriller };
                                ui.AddMovie(newMovie);
                                break;
                            case "4":
                                newMovie = new Movie() { Title = title, Genre = MovieGenre.Documentary };
                                ui.AddMovie(newMovie);
                                break;
                            case "5":
                                newMovie = new Movie() { Title = title, Genre = MovieGenre.SciFi };
                                ui.AddMovie(newMovie);
                                break;
                        }
                        Console.Clear();

                        Console.WriteAscii("MOVIE ADDED!", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        rgb = ResetColors(rgb);
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.Clear();
                        var list = ui.GetCustomers();
                        foreach (var item in list)
                        {
                            Console.WriteLine(item.FirstName + " " + item.LastName + " " + item.Ssn, Color.FromArgb(rgb[0], rgb[1], rgb[2]));

                            rgb[0] -= 38;
                            rgb[1] -= 19;
                        }
                        rgb = ResetColors(rgb);
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.Clear();
                        string getRentalsBySSN;
                        Console.WriteAscii("ENTER SSN OF RENTER", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        getRentalsBySSN = Console.ReadLine();
                        var getRentalsFor = ui.GetRentalsFor(getRentalsBySSN);
                        foreach (var item in getRentalsFor)
                        {
                            Console.WriteLine(item.MovieTitle + " " + item.DueDate, Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        }
                        break;
                    case "4":
                        Console.Clear();
                        string rentTitle;
                        string ssn;
                        Console.WriteAscii("ENTER TITLE", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        rentTitle = Console.ReadLine();
                        Console.Clear();
                        Console.WriteAscii("ENTER SSN OF RENTER", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        ssn = Console.ReadLine();
                        ui.RentMovie(rentTitle, ssn);
                        Console.WriteAscii("MOVIE RENTED!!", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        break;
                    case "5":
                        Console.Clear();
                        string returnTitle;
                        string returnSsn;
                        Console.WriteAscii("ENTER TITLE", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        returnTitle = Console.ReadLine();
                        Console.Clear();
                        Console.WriteAscii("ENTER SSN OF RENTER", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        returnSsn = Console.ReadLine();
                        ui.RentMovie(returnTitle, returnSsn);
                        Console.WriteAscii("MOVIE RETURNED!!", Color.FromArgb(rgb[0], rgb[1], rgb[2]));
                        break;
                }
            }

        }

        public static int[] ResetColors(int[] rgb)
        {
            int[] reset = rgb;
            rgb[0] = 154;
            rgb[1] = 255;
            rgb[2] = 255;
            return reset;
        }
    }

    public class Ui
    {
        //private IDateTime dateTime;
        //private IRentals iRentals;
        //private IVideoStore iVideoStore;
        //private VideoStore videoStore;
        //private VideoRentals videoRentals;
        private IVideoStore iVideoStore { get; set; }
        private IRentals iRentals { get; set; }
        public Ui(IVideoStore _videoStore, IRentals _rentals)
        {
            this.iVideoStore = _videoStore;
            this.iRentals = _rentals;
        }

        //public Ui()
        //{
        //    iVideoStore = new VideoStore(iRentals);
        //    iRentals = new VideoRentals(dateTime);
        //}
        public void AddMovie(Movie newMovie)
        {
            iVideoStore.AddMovie(newMovie);
        }

        public List<Customer> GetCustomers()
        {
            var list = iVideoStore.GetCustomers();
            return list;
        }

        public void GenerateData()
        {
            iVideoStore.RegisterCustomer(new Customer(
                "Johan",
                "Dole",
                "1920-02-11"));
            iVideoStore.RegisterCustomer(new Customer(
                "Goran",
                "Pettersson",
                "1850-09-22"));
            iVideoStore.RegisterCustomer(new Customer(
                "Emil",
                "Fagerberg",
                "1990-05-12"));
            iVideoStore.RegisterCustomer(new Customer(
                "John",
                "Doe",
                "1950-02-11"));
            iVideoStore.AddMovie(new Movie("Tarzan", MovieGenre.Comedy));

        }

        public void RentMovie(string title, string ssn)
        {
            iVideoStore.RentMovie(title, ssn);
        }

        public void ReturnMovie(string title, string ssn)
        {
            iRentals.RemoveRental(title, ssn);
        }

        public List<Rental> GetRentalsFor(string ssn)
        {
            var result = iRentals.GetRentalsFor(ssn);
            return result;
        }
    }
}
