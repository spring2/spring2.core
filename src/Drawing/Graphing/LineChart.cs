using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.UI;

/// derived from: Sample ASPX C# LineChart Class, Steve Hall, 2002

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// Creates a jpeg image with line charted information
    /// </summary>
    public class LineChart {

	private string title="Default Title";
	private string yAxisLabel="This is the Y axis";
	private string xAxisLabel="This is the X axis";
	private float yorigin=0;
	private float xorigin=0;
	private float scaleX = 0;  // use 0 to enable auto sizing
	private float scaleY = 0;  // use 0 to enable auto sizing
	private float xdivs=2;
	private float ydivs=2;
	private IList lines = new ArrayList();
	private int Width;
	private int Height;
	private Graphics graphics;
	private Bitmap bitmap;

	/// <summary>
	/// internal structure to keep track of the individual data points
	/// </summary>
	public struct datapoint {
	    public float x;
	    public float y;
	    public bool valid;
	}

	/// <summary>
	/// Creates a LineChart of x and y size
	/// </summary>
	/// <param name="myWidth"></param>
	/// <param name="myHeight"></param>
	/// <param name="myPage"></param>
	public LineChart(int myWidth, int myHeight) {
	    Width = myWidth; 
	    Height = myHeight;
	    ScaleX = myWidth; 
	    ScaleY = myHeight;
	    bitmap = new Bitmap(myWidth, myHeight);
	    graphics = Graphics.FromImage(bitmap);
	}

	/// <summary>
	/// Graph title
	/// </summary>
	public String Title {
	    get { return this.title; }
	    set { this.title = value; }
	}

	/// <summary>
	/// Label below chart for X axis
	/// </summary>
	public String XAxisLabel {
	    get { return this.xAxisLabel; }
	    set { this.xAxisLabel = value; }
	}

	/// <summary>
	/// left side label for Y axis
	/// </summary>
	public String YAxisLabel {
	    get { return this.yAxisLabel; }
	    set { this.yAxisLabel = value; }
	}

	/// <summary>
	/// Y origin
	/// </summary>
	public float YOrigin {
	    get { return yorigin; }
	    set { this.yorigin = value; }
	}

	/// <summary>
	/// X origin
	/// </summary>
	public float XOrigin {
	    get { return xorigin; }
	    set { this.xorigin = value; }
	}

	/// <summary>
	/// max x
	/// </summary>
	public float ScaleX {
	    get { return this.scaleX; }
	    set { this.scaleX = value; }
	}

	/// <summary>
	/// max y
	/// </summary>
	public float ScaleY {
	    get { return this.scaleY; }
	    set { this.scaleY = value; }
	}

	/// <summary>
	/// number of x axis divisions for axis labels
	/// </summary>
	public float XDivs {
	    get { return this.xdivs; }
	    set { this.xdivs = value; }
	}

	/// <summary>
	/// number of y axis divisions for axis labels
	/// </summary>
	public float YDivs {
	    get { return this.ydivs; }
	    set { this.ydivs = value; }
	}

	/// <summary>
	/// Add a new ChartLine to the LineChart
	/// </summary>
	/// <param name="line"></param>
	public void AddLine(ChartLine line) {
	    lines.Add(line);
	}


	/// <summary>
	/// Save output to a stream
	/// </summary>
	/// <param name="stream"></param>
	public void Save(Stream stream) {
	    Draw();
	    bitmap.Save(stream, ImageFormat.Jpeg);
	}

	/// <summary>
	/// Save image output to Page (convience for webpages)
	/// </summary>
	/// <param name="page"></param>
	public void Save(Page page) {
	    page.Response.ContentType="image/jpeg";
	    Save(page.Response.OutputStream);
	}

	private void Draw() {
	    Pen blackPen = new Pen(Color.Black,1);
	    Brush blackBrush = new SolidBrush(Color.Black);
	    Font axesFont = new Font("arial",10);
	    Font labelFont = new Font("arial",14);
	    Font legendFont = new Font("arial",8);

	    //first establish working area
	    graphics.FillRectangle(new SolidBrush(Color.LightYellow),0,0,Width,Height);

	    // margin defaults
	    int leftMargin = 50;
	    int rightMargin = 10;
	    int topMargin = 50;
	    int bottomMargin = 50;

	    // margin adjustments based on label and legend settings
	    if (!Title.Equals(String.Empty)) {
		topMargin += Size.Truncate(TextSize(Title, labelFont)).Height+1;
	    }

	    int adjustment = 0;
	    for(int i=0; i<lines.Count; i++) {
		ChartLine line = (ChartLine)lines[i];
		int w = Size.Truncate(TextSize(line.LegendText, legendFont)).Width+1;
		if (w>adjustment) {
		    adjustment = w;
		}
	    }
	    rightMargin += adjustment + 10;  // 10 for the colored boxes

	    if (!YAxisLabel.Equals(String.Empty)) {
		leftMargin += Size.Truncate(TextSize(YAxisLabel, labelFont)).Height+1;
	    }
	    if (!XAxisLabel.Equals(String.Empty)) {
		bottomMargin += Size.Truncate(TextSize(YAxisLabel, labelFont)).Height+1;
	    }

	    int ChartWidth = Width - leftMargin - rightMargin;
	    int ChartHeight = Height - topMargin - bottomMargin;
	    graphics.DrawRectangle(blackPen, leftMargin, topMargin, ChartWidth, ChartHeight);

	    // must draw all text items before doing the rotate below
	    // draw title
	    graphics.DrawString(Title, labelFont, blackBrush, (Width-TextSize(Title, labelFont).Width)/2, 10);

	    // X axis label
	    graphics.DrawString(XAxisLabel, labelFont, blackBrush, (Width-TextSize(XAxisLabel, labelFont).Width)/2, Height-10-TextSize(XAxisLabel, labelFont).Height);

	    // Y axis label
	    StringFormat sf = new StringFormat();
	    sf.FormatFlags = StringFormatFlags.DirectionVertical;
	    Font yfont = new Font(axesFont.FontFamily, 14, axesFont.Style, axesFont.Unit, axesFont.GdiCharSet, true);
	    graphics.DrawString(YAxisLabel, yfont, blackBrush, 10, 10, sf);

	    //draw X axis labels
	    for(int i=0; i<=XDivs; i++) {
		float x=leftMargin+(i*ChartWidth)/XDivs;
		float y=ChartHeight+topMargin;
		String myLabel = (XOrigin + (ScaleX*i/XDivs)).ToString();
		graphics.DrawString(myLabel, axesFont, blackBrush, x-4, y+10);
		graphics.DrawLine(blackPen, x, y+2, x, y-2);
	    }

	    //draw Y axis labels
	    for(int i=0; i<=YDivs; i++) {
		float x=leftMargin;
		float y=ChartHeight+topMargin-(i*ChartHeight/YDivs);
		String myLabel = (YOrigin + (ScaleY*i/YDivs)).ToString();
		graphics.DrawString(myLabel, axesFont, blackBrush, x-TextSize(myLabel, axesFont).Width, y-6);
		graphics.DrawLine(blackPen, x+2, y, x-2, y);
	    }

	    // draw legend
	    for(int i=0; i<lines.Count; i++) {
		ChartLine line = (ChartLine)lines[i];
		float x = Width-rightMargin;
		float y = topMargin + (float)(i * TextSize(line.LegendText, legendFont).Height * 1.5);
		graphics.FillRectangle(new SolidBrush(line.Color), x+5, y+5, 5, 5);
		graphics.DrawString(line.LegendText, legendFont, blackBrush, x+10, y);
	    }

	    //transform drawing coords to lower-left (0,0)
	    graphics.RotateTransform(180);
	    graphics.TranslateTransform(0,-Height);
	    graphics.TranslateTransform(-leftMargin,topMargin);
	    graphics.ScaleTransform(-1, 1);

	    //draw chart data
	    foreach(ChartLine line in lines) {
		datapoint prevPoint = new datapoint();
		prevPoint.valid=false;

		Pen pen = new Pen(line.Color,1);
		Brush brush = new SolidBrush(line.Color);

		foreach(datapoint myPoint in line.Points) {
		    if(prevPoint.valid==true) {
			float x0=ChartWidth*(prevPoint.x-XOrigin)/ScaleX;
			float y0=ChartHeight*(prevPoint.y-YOrigin)/ScaleY;
			float x=ChartWidth*(myPoint.x-XOrigin)/ScaleX;
			float y=ChartHeight*(myPoint.y-YOrigin)/ScaleY;
			graphics.DrawLine(pen,x0,y0,x,y);
			graphics.FillEllipse(brush,x0-2,y0-2,4,4);
			graphics.FillEllipse(brush,x-2,y-2,4,4);
		    }
		    prevPoint = myPoint;
		}
	    }

	    // uncomment to make sure that x/y coordinates are correct
	    //graphics.FillRectangle(new SolidBrush(Color.PowderBlue), 0, 0, 5, 5);
	    //graphics.FillRectangle(new SolidBrush(Color.PowderBlue), 0, (ChartHeight*(ScaleY-YOrigin)/ScaleY)-5, 5, 5);
	    //graphics.FillRectangle(new SolidBrush(Color.Plum), (ChartWidth*(ScaleX-XOrigin)/ScaleX)-5, (ChartHeight*(ScaleY-YOrigin)/ScaleY)-5, 5, 5);
	    //graphics.FillRectangle(new SolidBrush(Color.Plum), (ChartWidth*(ScaleX-XOrigin)/ScaleX)-5, 0, 5, 5);
	}


	/// <summary>
	/// Utility method to find the drawn size of a text string given an font
	/// </summary>
	/// <param name="text"></param>
	/// <param name="font"></param>
	/// <returns></returns>
	private SizeF TextSize(String text, Font font) {
	    Bitmap img = new Bitmap(1, 1);
	    Graphics tempGraphics = Graphics.FromImage(img);

	    return tempGraphics.MeasureString(text, font);
	}


	/// <summary>
	/// Class finalizer
	/// </summary>
	~LineChart() {
	    graphics.Dispose();
	    bitmap.Dispose();
	}

    }

}
