using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

// derived from: Sample ASPX C# LineChart Class, Steve Hall, 2002

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// This class represents each line that is drawn on a LineChart.  The internal points can be passed in, created from a constant
    /// y value, or created from a ResultSet.  The legend text passed in will be used when the legend is created.  The color is assigned
    /// when it is added to a LineChart.
    /// 
    /// Web page example:
    ///	    &lt;%@ Page Language="C#" %&gt;
    ///     &lt;%@ Import Namespace="System" %&gt;
    ///     &lt;%@ Import Namespace="System.Drawing" %&gt;
    ///     &lt;%@ Import Namespace="Spring2.Core.Drawing.Graphing" %&gt;
    /// 
    ///     &lt;script language="C#" runat="server"&gt;
    ///     void Page_Load(Object sender, EventArgs e) {
    ///     LineChart c = new LineChart(640, 480);
    ///     c.Title="Scorecard LineChart Test";
    /// 	
    ///     c.Title="";
    ///     c.XAxisLabel="";
    ///     c.YAxisLabel="";
    /// 	
    ///     c.XOrigin=0; 
    ///     c.ScaleX=500; 
    ///     c.XDivs=5;
    /// 	
    ///     c.YOrigin=0; 
    ///     c.ScaleY=1000; 
    ///     c.YDivs=5;
    /// 	
    ///     ChartLine line = new ChartLine();
    ///     line.LegendText = "line 1";
    ///     line.AddPoint(50,50);
    ///     line.AddPoint(100,100);
    ///     line.AddPoint(200,150);
    ///     line.AddPoint(450,450);
    ///     line.Color = Color.Blue;
    /// 	
    ///     ChartLine line2 = new ChartLine();
    ///     line2.LegendText = "line 2";
    ///     line2.AddPoint(50,150);
    ///     line2.AddPoint(100,200);
    ///     line2.AddPoint(200,250);
    ///     line2.AddPoint(450,550);
    ///     line2.Color = Color.Red;
    /// 
    ///     ChartLine line3 = new ChartLine();
    ///     line3.LegendText = "this is a really long legend";
    ///     line3.AddPoint(450,250);
    ///     line3.AddPoint(200,300);
    ///     line3.AddPoint(100,350);
    ///     line3.AddPoint(50,650);
    ///     line3.Color = Color.Purple;
    /// 	
    ///     c.AddLine(line);
    ///     c.AddLine(line2);
    ///     c.AddLine(line3);
    ///     c.Save(Page);
    /// }
    ///     &lt;/script&gt;
    /// </summary>
    public class ChartLine {

	private IList points = new ArrayList();
	private String legendText = String.Empty;
	private Color color = Color.Black;
	
	private float minX = float.MaxValue;
	private float maxX = float.MinValue;
	private float minY = float.MaxValue;
	private float maxY = float.MinValue;
	private Boolean showYValues = false;

	public ChartLine() {
	}

	public ChartLine(ArrayList points, String legendText) {
	    this.points = points;
	    this.legendText = legendText;
	}

	public ChartLine(int x1, int x2, float y, String legendText) {
	    this.legendText = legendText;

	    points = new ArrayList();
	    for(Int32 i=x1; i<=x2; i++) {
		LineChart.datapoint pd = new LineChart.datapoint();
		pd.x = i;
		pd.y = y;
		pd.valid = true;
		points.Add(pd);
		CalculateRange(pd);
	    }
	}

	public IList Points {
	    get { return points; }
	}


	public String LegendText {
	    get { return legendText; }
	    set { this.legendText = value; }
	}


	public Color Color {
	    get { return this.color; }
	    set { this.color = value; }
	}

	public void AddPoint(float x, float y) {
	    LineChart.datapoint dp = new LineChart.datapoint();
	    dp.x = x;
	    dp.y = y;
	    dp.valid = true;
	    points.Add(dp);
	    CalculateRange(dp);
	}

	public void AddPoint(float x, float y, Boolean valid) {
	    LineChart.datapoint dp = new LineChart.datapoint();
	    dp.x = x;
	    dp.y = y;
	    dp.valid = valid;
	    points.Add(dp);
	    CalculateRange(dp);
	}

	private void CalculateRange(LineChart.datapoint point) {
	    if (point.x<minX) {
		minX = point.x;
	    }
	    if (point.x>maxX) {
		maxX = point.x;
	    }
	    if (point.y<minY) {
		minY = point.y;
	    }
	    if (point.y>maxY) {
		maxY = point.y;
	    }
	}

	public float MinX {
	    get { return minX; }
	}

	public float MaxX {
	    get { return maxX; }
	}

	public float MinY {
	    get { return minY; }
	}

	public float MaxY {
	    get { return maxY; }
	}

	public Boolean ShowYValues {
	    get { return showYValues; }
	    set { this.showYValues = value; }
	}


    }
}