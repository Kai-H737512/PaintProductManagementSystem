using System;

namespace PMS.Models;

public class PaintSeries
{
  public int Id { get; set; }
  public string SeriesName { get; set; }
  public List<PaintProduct> PaintProducts { get; set; }

  public PaintSeries(string seriesName)
  {
    SeriesName = seriesName;
    PaintProducts = new List<PaintProduct>();
  }
}
