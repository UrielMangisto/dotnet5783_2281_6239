using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// the ID of the order
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// the status of the order
    /// </summary>
    public Enums.OrderStatus? Status { get; set; }
    /// <summary>
    /// the list of  all the dates the status changed 
    /// </summary>
    public List<TrackLst>? TrackList { get; set; } = new List<TrackLst>();

    public override string ToString()
    {
        string str = "Id: " + Id + "\nStatus: " + Status + "\nTracking:\n ";
        int i = 1;
        foreach (var tracking in TrackList ?? throw new Exception("The Track List is Empty"))
        {
            //str += i + ":\n" + tracking.Item1;
            //str += "\n" + tracking.Item2;
            str += i + ":\n" + tracking.Date;
            str += "\n" + tracking.Status;
            i++;
        }
        return str;
    }

}
public class TrackLst
{
    public DateTime? Date { get; set; }
    public string? Status { get; set; }
}
