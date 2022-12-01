using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    public int Id { get; set; }
    public Enums.OrderStatus Status { get; set; }
    public List<(DateTime?, string)> TrackList { get; set; }

    //to print the object
    public override string ToString()
    {
        string str = "Id: " + Id + "\nStatus: " + Status + "\nTracking:\n ";
        int i = 1;
        foreach (var tracking in TrackList)
        {
            str += i + ":\n" + tracking.Item1;
            str += "\n" + tracking.Item2;
            i++;
        }
        return str;
    }

}
