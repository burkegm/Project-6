using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Kiosk
{
    internal class Book
    {

        string title;
        string authorLastName;
        string authorFirstName;
        int pages;
        string publisher;

        public string[][] bookData = new string[211][];

        public Book(string title, string authorLastName, string authorFirstName, int pages, string publisher)
        {
            this.title= title;
            this.authorLastName= authorLastName;
            this.authorFirstName= authorFirstName;
            this.pages= pages;
            this.publisher= publisher;
        }
       
        public string getTitle()
        {
            return title;
        }
        
        public string getAuthorLastName()
        {
            return authorLastName;
        }

        public string getAuthorFirstName()
        {
            return authorFirstName;
        } 

        public int getPages()
        {
            return pages;
        }

        public string getPublisher()
        {
            return publisher;
        }

        public void Print()
        {
            Console.WriteLine("Title: " + title + "Author: " + authorLastName + ", " + authorFirstName + "Pages: " + pages + "Publisher: " + publisher);
        }

    }
}
