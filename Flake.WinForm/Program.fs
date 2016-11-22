open System
open System.Windows.Forms
open System.Drawing

let fillPixel (brush:Brush) (gr:Graphics) (point:Point) =
    gr.FillRectangle(brush, new Rectangle(point.X, point.Y,2,2))

[<EntryPoint>]
[<STAThread>]
let main argv = 
    Application.EnableVisualStyles()
    Application.SetCompatibleTextRenderingDefault false
    use form = new Form()
    form.BackColor <- Color.Black
    let snowFlake sendr obj =
        let g = form.CreateGraphics() 
        let drawPoint = fillPixel Brushes.White g
        let rec flake (center:Point) (angle:float) (size:int) (reduce_factor:float) = 
            drawPoint center
            let newSize = int (float size*reduce_factor)
            if newSize > 1 then
                let sides = int (360.0/angle)
                let calcOffset i f = Convert.ToInt32((float i) + (float size) * f(angle))
                let newCenterPt = new Point(calcOffset center.X Math.Cos, calcOffset center.Y Math.Sin)
                for x in 1 .. sides do
                    let incAngle = angle*(float x)
                    flake newCenterPt incAngle newSize reduce_factor
        flake (new Point(300,300)) 104.45 200 0.85
        g.Dispose()
    let handler = new EventHandler(snowFlake)
    form.Activated.AddHandler(handler)
    Application.Run(form)
    0 // return an integer exit code
