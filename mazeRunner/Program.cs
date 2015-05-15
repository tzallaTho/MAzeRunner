using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace mazeRunner
{
    class Program
    {
       // public enum DirectionOfWall { horizontal, vertical };
   
        static void Main(string[] args)
        {
            //the size of Maze
            int SizeOfMaze;
            TheMaze theMaze = new TheMaze();
       
            Console.WriteLine("<<<<<<####  <COPYRIGHTS TZALLA THEODORA ####<<<<<<<");
            Console.WriteLine("<<<ANY ATTEMPT OF UNAUTHORISED DOWNLOAD WILL BE A CRIME AGAINST DIGITAL LEGISLATION<<");
            Console.WriteLine("<<<< HAHAHAHAHAH <<<<");
            Console.WriteLine("<<<<<<<<<<<<<<<<<" + "WELCOME MAZE RUNNER" + "<<<<<<<<<<<<<<<<<<" );
            Console.WriteLine("Dear User please define size of Maze :");
            while (int.TryParse(Console.ReadLine(), out SizeOfMaze)==false)
            {
                Console.WriteLine("Not a formal input,Please provide me with a valid number :");
            }

            for(int i=0;i<SizeOfMaze*SizeOfMaze;i++)
            {
                theMaze.mazeList.Add(new mazePoint((int)Math.Floor((double)i / SizeOfMaze), i % SizeOfMaze));
            }
            Console.WriteLine("Size of Maze is constructed");
            //Walls input should be declared from a starting point which is smaller
            //than the point terminating the wall
            Console.WriteLine("Now give me the walls as : \n" +
            "Starting Point P:x,y sizeOfCells N:number WallDirection D[h for horizontal v for vertical] \n");
            Console.WriteLine("    EXAMPLE :      P:2,2  N:3  D:v \n");
            Console.WriteLine("You can put as many walls you want but beware not \n to make a deadlock,you can seperate the"+
                "input with new line feed and \n end wall input with END keyword on a new Line");

            //a regex expreesion to validate on the wall input data
            //string pattern = @"^(P|p)?:[0-" + SizeOfMaze + "][,][0-" + SizeOfMaze +
                                  //  "] +[Nn]{1}?:[0-"+ SizeOfMaze + "] +[Dd]{1}?:[vVhH]{1}?$";
            string pattern = @"^(P|p)?:(\d),(\d) +(N|n)?:(\d) +(D|d):(V|v|H|h)$";
            
            //var tempList;
            string line= " ";
            while (!(line = Console.ReadLine()).Contains("END"))
            {
                Regex rgx = new Regex(pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant);
                foreach (Match match in rgx.Matches(line))
                {
                    theMaze.Constructwalls(new mazePoint(Int32.Parse(match.Groups[2].Value), Int32.Parse(match.Groups[3].Value)),
                                      Int32.Parse(match.Groups[5].Value),
                                      ((string)match.Groups[7].Value == "h") ? DirectionOfWall.horizontal : DirectionOfWall.vertical);
                 }
            }

           
            Console.WriteLine(" Now it's time for Start and Finish buttons :  EXAMPLE S:2,2  G:5,5 \n");
            string pattern2 = @"^(S|s)?:(\d),(\d) +(G|g)?:(\d),(\d)$";
          
            string line2 = Console.ReadLine();
            Regex rgx2 = new Regex(pattern2, RegexOptions.Multiline | RegexOptions.CultureInvariant);

            //check for valid input and that the point is not on a wall
            while (rgx2.Matches(line2 = Console.ReadLine()).Count == 0)  
            
               // theMaze.isPointOnWall((theMaze.StartPoint = new mazePoint(Int32.Parse(rgx2.Matches(line2)[0].Groups[2].Value), 
                                 //   Int32.Parse(rgx2.Matches(line2)[0].Groups[3].Value))))
                //|| theMaze.isPointOnWall((theMaze.TargetPoint = new mazePoint(Int32.Parse(rgx2.Matches(line2)[0].Groups[5].Value), 
                                   // Int32.Parse(rgx2.Matches(line2)[0].Groups[6].Value)))))
            { 
            theMaze.StartPoint = new mazePoint(Int32.Parse(rgx2.Matches(line2)[0].Groups[2].Value), 
                                    Int32.Parse(rgx2.Matches(line2)[0].Groups[3].Value));

            theMaze.TargetPoint = new mazePoint(Int32.Parse(rgx2.Matches(line2)[0].Groups[5].Value), 
                                    Int32.Parse(rgx2.Matches(line2)[0].Groups[6].Value));
            
            Console.WriteLine(" Unless you give the points we cannot start :  EXAMPLE S:2,2  G:5,5 \n");  }


            theMaze.StartMoving();
          
              
        }
      
    }
}
