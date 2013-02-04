using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SpaceShip
{
    class Control
    {
        int idPlayer;
        int deltaAngle;
        int deltaSpeedX;
        int deltaSpeedY;
        bool shooting;
        bool braking;

        static Control instance=null;

        public enum ControlType{
            Traditional
        }

        ControlType type;

        public Control(ControlType type)
        {
            if (instance == null)
            {
                instance = this;
                this.type = type;
            }
            
        }

        public static Control getInstance()
        {
            return Control.instance;
        }

        public void captureControls()
        {
            int dSpeedY = 0, dAngle=0;
            bool shoot = false;

            if (type == ControlType.Traditional)
            {
                if (Keyboard.IsKeyDown(Key.Up)) dSpeedY++;
                if (Keyboard.IsKeyDown(Key.Down)) dSpeedY--;
                if (Keyboard.IsKeyDown(Key.Left)) dAngle++;
                if (Keyboard.IsKeyDown(Key.Right)) dAngle--;
                if (Keyboard.IsKeyDown(Key.Space)) shoot = true;

                this.setDeltaSpeedY(dSpeedY);
                this.setDeltaAngle(dAngle);
                this.setShooting(shoot);   
            }
        }

        public int getIdPlayer()
        {
            return this.idPlayer;
        }

        public void setIdPlayer(int idPlayer)
        {
            this.idPlayer = idPlayer;
        }

        public int getDeltaAngle()
        {
            return this.deltaAngle;
        }

        protected void setDeltaAngle(int dA)
        {
            this.deltaAngle = dA;
        }

        public int getDeltaSpeedX()
        {
            return this.deltaSpeedX;
        }

        protected void setDeltaSpeedX(int dSX)
        {
            this.deltaSpeedX = dSX;
        }

        public int getDeltaSpeedY()
        {
            return this.deltaSpeedY;
        }

        protected void setDeltaSpeedY(int dSY)
        {
            this.deltaSpeedY = dSY;
        }

        public bool isShooting()
        {
            return this.shooting;
        }

        protected void setShooting(bool condition)
        {
            this.shooting = condition;
        }

        public bool isBraking()
        {
            return this.braking;
        }

        protected void setBraking(bool condition)
        {
            this.braking = condition;
        }

        public override string ToString()
        {

            return "dsY: " + deltaSpeedY + " braking: " + braking + " dAngle: " + deltaAngle + " shooting: " + shooting;
        }
    }
}

