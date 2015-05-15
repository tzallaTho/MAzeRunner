using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazeRunner
{
    public delegate void WallReachedEventHandler(object sender, WallReachedEventArgs e);

     public delegate void EscapeMovesEventHandler(object sender,  EscapeMovesEventArgs e);

    class Path
    {        
       public List<mazePoint> thePoints = new List<mazePoint>();
       public int height=0;
       EscapeMethod escapeMeth= new EscapeMethod();
       private bool _hitOnWall = false;
       public event WallReachedEventHandler WallReached;

       public event EscapeMovesEventHandler StartEscaping; 
        //public event WallReachedEventHandler StartEscaping;

       public mazePoint CurrentPoint;
       
       public Path(mazePoint ss)
       {
           thePoints.Clear();
           //set starting point of the path
           this.CurrentPoint = ss;
           thePoints.Add(CurrentPoint);
          
       }
        public void AddPoint(mazePoint dd)
        {
            thePoints.Add(dd);
            height++;
           
        }
        public void AddEscapeMoves(typeOfMove tt)
        {
            escapeMeth.alternatives.Add(tt);
        }
        public void ClearEscapeMoves()
        {
            escapeMeth.alternatives.Clear();
        }

         public bool HitOnWall
        {
            get { return _hitOnWall; }
            set { _hitOnWall = value; }
        }
        public void PointMove(typeOfMove tt)
        {
            if (_hitOnWall) //an xtipise se toixo steile event
            {
               // _hitOnWall = false;
               //dwse tis plirofories p eisai k steile event
                WallReachedEventArgs e = new WallReachedEventArgs( this.CurrentPoint, tt,DirectionOfWall.none);
              //  NumberReachedEventArgs e = new NumberReachedEventArgs(reachableNum);
                OnWallReached(e);
                return;
            }
            escapeMeth.alternatives.Add(tt);
            this.CurrentPoint.MakeAMove(tt);
            this.AddPoint(CurrentPoint);
           // return this.CurrentPoint;
        }

          public typeOfMove EscapeWallAlt(DirectionOfWall dd)
        {
            typeOfMove alterType=typeOfMove.none;
              List<typeOfMove> alters = new List<typeOfMove>();

            if (dd == DirectionOfWall.horizontal)
            {
                alters.Add(typeOfMove.right);
                alters.Add(typeOfMove.left);
            }

               var leftOuterQuery =
           (from al in alters
           join ee in escapeMeth.alternatives on al equals ee into ggroup
                      select ggroup.DefaultIfEmpty()).First();
                      //.DefaultIfEmpty({ Name = "Nothing!", CategoryID = al });
          
          
            foreach (typeOfMove tt2 in Enum.GetValues(typeof(typeOfMove)))
            {
            if(escapeMeth.alternatives.Where(g => g == tt2).Count() == 0)
                {
                //select next alternative way
                    alterType = (typeOfMove)tt2;
                }
            }
           // escapeMeth.alternatives.Add(alterType);
            return alterType;  
        }


          public void PathEscaping(int numOfSteps,typeOfMove ff)
          {
              //the alternative way is found here
             // typeOfMove ff = EscapeWallAlt();
               EscapeMovesEventArgs d= new EscapeMovesEventArgs(numOfSteps,ff);
               OnStartEscaping(d);
               return;
           }

          public void RecursiveEscape(typeOfMove ff)
          {
            //  this.PointMove(ff);
             // f.MakeAMove(tt);
             // Console.WriteLine(" \n One more step to target point : \n " + f.MyX + " , " + f.MyY);

              // if (isPointOnWall(f))
              // {
              //if you hit on another wall then determine another way out
              // RecursiveEscape(f, thePath.EscapeWallAlt(tt));
              //}
              //continue escaping
           //   RecursiveEscape(f, tt);
          }
          //ovveride on wallreached
          protected virtual void OnWallReached(WallReachedEventArgs e)
          {
              if (WallReached != null)
              {
                  WallReached(this, e);
              }
          }

          protected virtual void OnStartEscaping(EscapeMovesEventArgs e)
          {
              if (StartEscaping != null)
              {
                  StartEscaping(this, e);
              }
          }
       
    }

    class EscapeMethod
    {
        public List<typeOfMove> alternatives = new List<typeOfMove>();
    }

    #region WallArgs
    //Wall info to pass around on event triggered
    public class WallReachedEventArgs : EventArgs
    {
       public mazePoint _pp; //i was in the current point of +pp
        public typeOfMove _type;//i was moving like _type
        public DirectionOfWall _wallDirect;
        public WallReachedEventArgs( mazePoint pp,  typeOfMove t,DirectionOfWall dd)
        {
            this._pp = pp;
             this._type = t;
             this._wallDirect = dd;
        }
        //public int ReachedWall
        //{
            //get{return _wallId;}
        //}
    }
    #endregion 

     #region EscapeMovesArges
    //Wall info to pass around on event triggered
    public class EscapeMovesEventArgs : EventArgs
    {
        public int _numOfStepsToescape;
        public typeOfMove _type;
        public EscapeMovesEventArgs( int noStep, typeOfMove t)
        {
            this._numOfStepsToescape = noStep;
            this._type = t;
        }
    }
    #endregion 
   
}
