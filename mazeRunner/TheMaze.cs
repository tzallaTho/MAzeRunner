using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazeRunner
{
   
     public enum DirectionOfWall { horizontal, vertical, none};
     #region Maze
     class TheMaze
    {
         /// <summary>
         /// The main Class that holds the necessary Info
         /// </summary>      
        public mazePoint StartPoint= new mazePoint();
        public mazePoint TargetPoint = new mazePoint();
        public List<mazePoint> mazeList;
        public List<Wall> mazeWalls;

       
        //event declaration for wall hit
       // public event WallReachedEventHandler WallReached;

       // public Path thePath = new Path();

        public TheMaze()
        {
            mazeList = new List<mazePoint>();
            mazeWalls = new List<Wall>();
        }

        public void MoveAccording(MovingClass ff, Path cpath)
        {
            int nn = 0;
            while (nn < ff._cellDiff)
            {//as long as we dont hit the wall move like the comparer told you
                // StartPoint.MakeAMove(ff._type,1); //make one step at a time
                cpath.PointMove(ff._type);
                isPointOnWall(cpath);
                nn++;

            }
        }
         
        public void StartMoving()
        {
            List<MovingClass> ways = StartPoint.CompareTo(TargetPoint);
            Path thePath = new Path(StartPoint);
            thePath.WallReached += new WallReachedEventHandler(EscapePlanNEW);
            thePath.StartEscaping += new EscapeMovesEventHandler(startEscapingHandler);
         
            foreach (MovingClass ff in ways)
            {
              MoveAccording(ff, thePath);
            }

            Console.ReadLine();
        }

        public void Constructwalls(mazePoint sf, int sz, DirectionOfWall dd)
        {
            //update the list according to wall selection
            //so as to define if a specic cell is Wall and cuts a path to the target Point
            //we have two options horizontal or vertical wall
          //  DirectionOfWall d = ((string)dd == "h") ? DirectionOfWall.horizontal : DirectionOfWall.vertical;
            if (dd.Equals(DirectionOfWall.horizontal))
            {   // this is the wall  #####..sz
                (from point in mazeList
                 where point.MyY == sf.MyY && point.MyX >= sf.MyX && point.MyX < sf.MyX + sz
                 select point).ToList().ForEach(point => point.IsWallCell = true);
           }
            else
            {// this is the wall    ##
                //                  ##
                //                  .
                //                  .
                //                  sz
                (from point in mazeList
                 where point.MyX == sf.MyX && point.MyY >= sf.MyY && point.MyY < sf.MyY + sz
                 select point).ToList().ForEach(point => point.IsWallCell = true);

            }
            //add wall info to the List of walls
             mazeWalls.Add(new Wall(sf,sz,dd));
        }

        public void EscapePlanNEW(object sender, WallReachedEventArgs e)
        {
            int numOfSteps=-1;
            DirectionOfWall yy = DirectionOfWall.none;
            //Path bb =sender as Path;
            mazePoint bb = e._pp;  //the point that hit on the wall

            int tempf1=-1,tempf2=-1;
          
            tempf1 = (from pp in mazeWalls
                      where bb.MyX == pp.StartWallPoint.MyX && bb.MyY >= pp.StartWallPoint.MyY
                                     && bb.MyY < pp.StartWallPoint.MyY + pp.SzOfCells
                                      && pp.WallDirection == DirectionOfWall.vertical
                                        select new { sizeofSteps = Math.Abs(pp.SzOfCells - bb.MyY) }).First().sizeofSteps;
                  

            if (tempf1 != -1)
            {
               //the wall met is vertical so first alternative is bottom
                yy = DirectionOfWall.vertical;
                //WallIdenti = f.WallIdentifier;

            }

            //there is a case that a point belongs at multiple walls
            tempf2 = (from pp in mazeWalls
                     where bb.MyY == pp.StartWallPoint.MyY && bb.MyX >= pp.StartWallPoint.MyX
                                                && bb.MyX < pp.StartWallPoint.MyX + pp.SzOfCells
                                                       && pp.WallDirection == DirectionOfWall.horizontal
                      select new { sizeofSteps = Math.Abs(pp.SzOfCells - bb.MyX) }).First().sizeofSteps;

            if (tempf2 != -1)
            {
                yy = DirectionOfWall.horizontal;

            }
            (sender as Path).PathEscaping(numOfSteps,typeOfMove.none);
      
        }

        public void startEscapingHandler(object sender, EscapeMovesEventArgs w)
        {
           // int cnt = 0;
            MovingClass ff = new MovingClass(w._type,w._numOfStepsToescape);
            MoveAccording(ff, sender as Path);
        }
         //return size of Wall cells and the way to escape the wall
         /*
        public void  EscapePlan(mazePoint meetingPoint,ref DirectionOfWall tt,ref int numOfSteps,ref int WallIdenti)
        {
            numOfSteps = -1;
            //in case the wall hitten is vertical
            numOfSteps = (from pp in mazeWalls
                          where meetingPoint.MyX == pp.StartWallPoint.MyX && meetingPoint.MyY >= pp.StartWallPoint.MyY && meetingPoint.MyY < pp.StartWallPoint.MyY + pp.SzOfCells
                                                         && pp.WallDirection == DirectionOfWall.vertical
                          select new { sizeofSteps = Math.Abs(pp.SzOfCells - meetingPoint.MyY) }).First().sizeofSteps;
            
           if (numOfSteps != -1)
            {
                  tt = DirectionOfWall.vertical;
                 
            }
             //in case the wall hitten is horizontal
           numOfSteps = (from pp in mazeWalls
                       where meetingPoint.MyY == pp.StartWallPoint.MyY && meetingPoint.MyX >= pp.StartWallPoint.MyX && meetingPoint.MyX < pp.StartWallPoint.MyX + pp.SzOfCells
                                                      && pp.WallDirection == DirectionOfWall.vertical
                         select new { sizeofSteps = Math.Abs(pp.SzOfCells - meetingPoint.MyX)}).First().sizeofSteps;


              if (numOfSteps != -1)
              {
                  tt = DirectionOfWall.horizontal;

              }
             }*/

       
        public bool isPointOnWall(Path currentPath)
        {
            bool indeedAPoint= (from pp in mazeList
                                where currentPath.CurrentPoint.MyX == pp.MyX && currentPath.CurrentPoint.MyY == pp.MyY && pp.IsWallCell
                    select pp).Count() == 1 ? true : false;

            if (indeedAPoint)
            //we have to trigger an event
            {
                currentPath.HitOnWall = true;
            }
            return indeedAPoint;
        }
    }
     #endregion

    

     #region Wall
     /// <summary>
     /// A wall is constructed from a starting point and it is extended according to
     /// size of cells and a direction defined by user
     /// </summary>
     class Wall
     {
         public static int wallId;
        public mazePoint StartWallPoint;
         public int SzOfCells;
         public DirectionOfWall WallDirection;
         public int WallIdentifier;
         public Wall()
         {
             wallId = -1;
         }

         public Wall(mazePoint dd, int sz, DirectionOfWall f)
         {
             StartWallPoint = dd;
             SzOfCells = sz;
             WallDirection = f;
             WallIdentifier = ++wallId;
         }


     }
  #endregion

}
