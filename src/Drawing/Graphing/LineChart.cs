using System;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

// derived from: Sample ASPX C# LineChart Class, Steve Hall, 2002

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// Creates a jpeg image with line charted information
    /// </summary>
    public class LineChart {

	private string title="Default Title";
	private string yAxisLabel="This is the Y axis";
	private string xAxisLabel="This is the X axis";
	private float yorigin=0;    // use 0 to enable auto sizing
	private float xorigin=0;    // use 0 to enable auto sizing
	private float scaleX = 0;   // use 0 to enable auto sizing
	private float scaleY = 0;   // use 0 to enable auto sizing
	private float xdivs=8;	    // use 0 to enable auto sizing
	private float ydivs=8;	    // use 0 to enable auto sizing
	private IList lines = new ArrayList();
	private int Width;
	private int Height;
	private Graphics graphics;
	private Bitmap bitmap;
	private LabelFormat xformat = new NumberLabelFormat();
	private LabelFormat yformat = new NumberLabelFormat();

	// margin defaults
	private int leftMargin = 50;
	private int rightMargin = 10;
	private int topMargin = 50;
	private int bottomMargin = 50;

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
	public LineChart(int myWidth, int myHeight) {
	    Width = myWidth; 
	    Height = myHeight;
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
	/// X axis labels
	/// </summary>
	public LabelFormat XLabelFormat {
	    get { return this.xformat; }
	    set { this.xformat = value; }
	}

	/// <summary>
	/// Y axis labels
	/// </summary>
	public LabelFormat YLabelFormat {
	    get { return this.yformat; }
	    set { this.yformat = value; }
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
	/// <param name="format"></param>
	public void Save(Stream stream, ImageFormat format) {
	    Draw();
	    bitmap.Save(stream, format);
	}


	private void Draw() {
	    Pen blackPen = new Pen(Color.Black,1);
	    Brush blackBrush = new SolidBrush(Color.Black);
	    Font axesFont = new Font("arial",8);
	    Font labelFont = new Font("arial",14);
	    Font legendFont = new Font("arial",8);

	    //first establish working area
	    graphics.FillRectangle(new SolidBrush(Color.LightYellow),0,0,Width,Height);

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
		Int32 h = Size.Truncate(TextSize(XAxisLabel, labelFont)).Height+1;
		bottomMargin += Convert.ToInt32(h*1.0);
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

	    CalculateScale();
	    if (ScaleX-XOrigin<XDivs) {
		XDivs = ScaleX-XOrigin;
	    }

	    //draw X axis labels
	    for(int i=0; i<=XDivs; i++) {
		float x=leftMargin+(i*ChartWidth)/XDivs;
		float y=ChartHeight+topMargin;
		float div = (XOrigin + (ScaleX*i/XDivs));
		String myLabel = String.Empty;
		if (xformat!= null) {
		    myLabel = xformat.GetLabel(div);
		} else {
		    myLabel = div.ToString();
		}
		StringFormat csf = new StringFormat();
		csf.Alignment = StringAlignment.Center;
		graphics.DrawString(myLabel, axesFont, blackBrush, x-4, y+10, csf);
		graphics.DrawLine(blackPen, x, y+2, x, y-2);
	    }

	    //draw Y axis labels
	    for(int i=0; i<=YDivs; i++) {
		float x=leftMargin;
		float y=ChartHeight+topMargin-(i*ChartHeight/YDivs);
		float div = (YOrigin + (ScaleY*i/YDivs));
		String myLabel;
		if (yformat!=null) {
		    myLabel = yformat.GetLabel(div);
		} else {
		    myLabel = div.ToString();
		}
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

	    // draw y values
	    foreach(ChartLine line in lines) {
		if (line.ShowYValues) {
		    Pen pen = new Pen(line.Color,1);
		    Brush brush = new SolidBrush(line.Color);

		    foreach(datapoint point in line.Points) {
			if (point.valid) {
			    float x=ChartWidth*(point.x-XOrigin)/ScaleX;
			    float y=ChartHeight*(point.y-YOrigin)/ScaleY;
			    String label = yformat.GetLabel(point.y);

			    // adjust
			    y = ChartHeight-y + topMargin;
			    x += leftMargin;
			    StringFormat csf = new StringFormat();
			    csf.Alignment = StringAlignment.Center;
			    graphics.DrawString(label, axesFont, blackBrush, x, y-(TextSize(label, axesFont).Height*1.5F), csf);
			}
		    }
		}
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

		foreach(datapoint point in line.Points) {
		    if(point.valid) {
			float x0=ChartWidth*(prevPoint.x-XOrigin)/ScaleX;
			float y0=ChartHeight*(prevPoint.y-YOrigin)/ScaleY;
			float x=ChartWidth*(point.x-XOrigin)/ScaleX;
			float y=ChartHeight*(point.y-YOrigin)/ScaleY;
			if (prevPoint.valid) {
			    graphics.DrawLine(pen,x0,y0,x,y);
			    graphics.FillEllipse(brush,x0-2,y0-2,4,4);
			}
			graphics.FillEllipse(brush,x-2,y-2,4,4);
		    }
		    prevPoint = point;
		}
	    }
	}

	private void CalculateScale() {
	    // only calculate if both are set to 0
	    if (scaleX==0 && scaleY==0) {
		// reset values
		xorigin = float.MaxValue;
		scaleX = float.MinValue;
		yorigin = float.MaxValue;
		scaleY = float.MinValue;
	
		foreach(ChartLine line in lines) {
		    if (line.MinX<xorigin) {
			xorigin = line.MinX;
		    }
		    if (line.MaxX>scaleX) {
			scaleX = line.MaxX;
		    }
		    if (line.MinY<yorigin) {
			yorigin = line.MinY;
		    }
		    if (line.MaxY>scaleY) {
			scaleY = line.MaxY;
		    }
		}

		// if there were no points, set a default
		if (xorigin==float.MaxValue && scaleX==float.MinValue && yorigin==float.MaxValue && scaleY==float.MinValue) {
		    xorigin=0;
		    yorigin=0;
		    scaleX=10;
		    scaleY=10;
		} else {
		    //yorigin *= 0.9F;
		    scaleY *= 1.1F;
		}
	    }
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
