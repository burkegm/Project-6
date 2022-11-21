namespace Library_Kiosk
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = "C:\\Users\\Grays\\Desktop\\project 6\\Library Kiosk\\Library Kiosk\\books.csv";
            StreamReader sr = new StreamReader(file);
            Tree<Book> AVLTree = new();
            int lineCounter = 0;
            while (sr.EndOfStream != true)
            {
                string line = sr.ReadLine();
                //Console.WriteLine(line);
                lineCounter++;
            }

            string[][] bookData = new string[lineCounter][];

            for(int i = 0; i < lineCounter; i++)
            {
                bookData[i] = splitLine(file, lineCounter, i + 1);
            }

            for (int i = 0; i < bookData.Length; i++)
            {
                string title = bookData[i][0]; 
                string authorLastName  = bookData[i][1];
                string authorFirstName = bookData[i][2];
                int pages = Convert.ToInt32(bookData[i][3]);
                string publisher = bookData[i][4];

                Book book = new Book(title, authorLastName, authorFirstName, pages, publisher);

                AVLTree.Add(book);
            }
            
        }

        /// <summary>
        /// https://stackoverflow.com/questions/1262965/how-do-i-read-a-specified-line-in-a-text-file
        /// Returs a specific line in a file
        /// </summary>
        /// <param name="fileName"> the file that you want to read from</param>
        /// <param name="line">the line of the file you want returned</param>
        /// <returns></returns>
        static string GetLine(string fileName, int line)
        {
            using (var sr = new StreamReader(fileName))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }

        static string[] splitLine(string file, int lineCounter, int counter)
        {
            string[] parts = new string[5];
            


            string record = GetLine(file,counter);
            parts = record.Split(",");
            List<string> cleanParts = new List<string>();
            string cleanPart = "";
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "")
                    parts[i] = " ";
  
                cleanPart += parts[i];
                if (cleanPart[0] == '\"' && cleanPart[cleanPart.Length - 1] != '\"')
                {
                    cleanPart += ",";
                    continue;
                }
                cleanParts.Add(cleanPart);
                cleanPart = "";
                        
            }
            //foreach (var part in cleanParts)
            //{
            //    Console.WriteLine(part);
            //}
            
            return parts;
            
        }
    }
}