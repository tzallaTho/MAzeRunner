using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mazeRunner
{
    public enum typeOfMove { top, bottom, left, right, none };
    public class mazePoint
    {
       
        int _xCoord;
        int _yCoord;
        bool _isWallCell = false;
        public typeOfMove tMove;
       // public int belongsToWall;
        public mazePoint(int x = -1, int y = -1)
        {
            this._xCoord = x;
            this._yCoord = y;
        }
        public int MyX
        {
            get { return _xCoord; }
        }
        public int MyY
        {
            get { return _yCoord; }
        }
        public bool IsWallCell
        {
            get { return _isWallCell; }
            set { _isWallCell = value; }
        }

        public void MakeAMove(typeOfMove type)
        {
            switch (type)
            {
                case typeOfMove.top:
                    this.MoveTop();
                    break;
                case typeOfMove.bottom:
                    this.MoveBottom();
                    break;
                case typeOfMove.right:
                    this.MoveRight();
                    break;
                case typeOfMove.left:
                    this.MoveLeft();
                    break;
                default:
                    break;
            }

           

            Console.WriteLine(" \n I just moved  " + type.ToString() + " and i am now at " + this.MyX + " " + this.MyY);
        }

       

        //from mypoint going to other and find which ways to the target according to ccordinates difference
        public List<MovingClass> CompareTo(mazePoint other)
        {
            List<MovingClass> list = new List<MovingClass>();
             
            if (this.MyX == other.MyX && this.MyY > other.MyY)
            {
                list.Add(new MovingClass(typeOfMove.bottom, Math.Abs(MyY - other.MyY)));
                return list;
            }
            else if (this.MyX == other.MyX && this.MyY < other.MyY)
            {
                list.Add(new MovingClass(typeOfMove.top, Math.Abs(MyY - other.MyY)));
                return list;
            }
            else if (this.MyY == other.MyY && this.MyX > other.MyX)
            {
                list.Add(new MovingClass(typeOfMove.left, Math.Abs(MyY - other.MyY)));
                return list;
            }
            else if (this.MyY == other.MyY && this.MyX > other.MyX)
            {
                list.Add(new MovingClass(typeOfMove.right, Math.Abs(MyY - other.MyY)));
                return list;
            }
            else if (this.MyX > other.MyX && this.MyY > other.MyY)
            {
                list.Add(new MovingClass(typeOfMove.bottom, Math.Abs(MyY - other.MyY)));
                list.Add(new MovingClass(typeOfMove.left, Math.Abs(MyX - other.MyX)));
                return list;
            }
            else if(this.MyX > other.MyX && this.MyY < other.MyY)
            {
                list.Add(new MovingClass(typeOfMove.top, Math.Abs(other.MyY - this.MyY)));
                list.Add(new MovingClass(typeOfMove.left, Math.Abs(other.MyX - this.MyX)));
                return list;
            }

           
            //else if (this.MyY > other.MyY && this.MyX < other.MyX)
             //   return MoveRight();
            //else if (this.MyY > other.MyY && this.MyX > other.MyX)
             //   return MoveBottom();
            return list;
            #region old
            /*
            if (this.MyX == other.MyX && this.MyX == other.MyY)
                return 100;
            if (this.MyX == other.MyX && this.MyY > other.MyY)
                return MoveBottom();
            else if (this.MyX == other.MyX && this.MyY < other.MyY)
                return MoveTop();
            else if (this.MyY == other.MyY && this.MyX > other.MyX)
                return MoveLeft();
            else if (this.MyY == other.MyY && this.MyX < other.MyX)
                return MoveRight();
            else if (this.MyX > other.MyX && this.MyY > other.MyY)
                return MoveBottom();
            else if (this.MyX > other.MyX && this.MyY < other.MyY)
                return MoveLeft();
            else if (this.MyY > other.MyY && this.MyX < other.MyX)
                return MoveRight();
            else if(this.MyY > other.MyY && this.MyX > other.MyX)
                return MoveBottom();
            return -1;
          */
            #endregion
        }
         int MoveRight() { this.tMove = typeOfMove.right; this._xCoord += 1; return 1; }
         int MoveLeft() { this.tMove = typeOfMove.left; this._xCoord -= 1; return 2; }
         int MoveTop() { this.tMove = typeOfMove.top; this._yCoord += 1; return 3; }
         int MoveBottom() { this.tMove = typeOfMove.bottom; this._yCoord -= 1; return 4; }
        /*public int MoveRightAndBottom() { MoveRight();MoveBottom(); return 5; }
        public int MoveRightAndTop() { MoveRight(); MoveTop(); return 6; }
        public int MoveLeftAndBottom() { MoveLeft(); MoveBottom(); return 7; }
        public int MoveLeftAndTop() { MoveLeft(); MoveTop(); return 8; }
        public int MoveBottomAndRight() { MoveBottom(); MoveRight(); return 9; }
        public int MoveBottomAndLeft() { MoveBottom(); MoveLeft(); return 10; }
        public int MoveTopAndLeft() { MoveTop(); MoveLeft(); return 11; }
        public int MoveTopAndRight() { MoveTop(); MoveRight(); return 12; }*/
    }

    public class MovingClass
    {
        public typeOfMove _type;
        public int _cellDiff;
        public MovingClass(typeOfMove tt, int cc)
        {
            _type = tt;
            _cellDiff = cc;
        }
    }

  
}


