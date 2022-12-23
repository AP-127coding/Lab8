using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    interface IShoot //стрельба танков
    {
        int[] Shoot(Map map, Tank tank);
    }
    interface IMovement
    {
        void Movement(Map map, Tank tank, char k); // движение танков
    }

}
