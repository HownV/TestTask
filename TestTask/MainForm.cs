using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TestTask.Interfaces;
using TestTask.Models;
using TestTask.Models.EventArgs;
using TestTask.Models.Exceptions;
using Rectangle = TestTask.Models.Rectangle;

namespace TestTask
{
    public partial class MainForm : Form
    {
        private readonly ResourceManager _resourceManager;
        private readonly Pen _pen;

        
        private IList<Figure> _figures;
        private IList<bool> _isRun;

        public MainForm()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            _resourceManager = new ResourceManager("TestTask.MainForm", typeof(MainForm).Assembly);
            _pen = new Pen(Color.Black);
            _figures = new SynchronizedCollection<Figure>();
            _isRun = new SynchronizedCollection<bool>();
            
            InitializeComponent();
            
            cbLocalization.SelectedIndex = 0;

            new Thread(delegate() 
            {
                while (true)
                {
                    Thread.Sleep(14);
                    pbMain.Refresh();
                }
            }).Start();
        }
        
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            if (_figures.Count > 0)
            {
                Graphics g = e.Graphics;
                for (int i = 0; i < _figures.Count; i++)
                {
                    for (int j = 0; j < _figures.Count; j++)
                    {
                        if (i == j)
                            continue;
                        Intersect(i, j);
                    }
                }

                for (int i = 0; i < _figures.Count; i++)
                {
                    if (_isRun[i])
                    {
                        try
                        {
                            _figures[i].Move(pbMain.Width, pbMain.Height);
                        }
                        catch (FigureOutOfRangeException exception)
                        {
                            _isRun[i] = false;
                            _figures[i].PutIntoCorrectPlace(_figures, _isRun, i, pbMain.Width, pbMain.Height);
                            // ловлю что-то, пишу в логи 
                        }
                    }
                    _figures[i].Draw(g, _pen);
                }
            }
        }

        private void Intersect(int i, int j)
        {
            // пересечение (в основном) слева направо
            if (_figures[i].X < _figures[j].X && _figures[i].RightBorder > _figures[j].X)
            {
                // левый верхний угол
                if (_figures[i].Y < _figures[j].Y && _figures[i].BottomBorder > _figures[j].Y)
                {
                    // пересечение по X
                    if (_figures[i].RightBorder - _figures[j].X < _figures[i].BottomBorder - _figures[j].Y)
                    {
                        _figures[i].ReverseDx(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].RightBorder,
                            Y = _figures[i].BottomBorder - _figures[j].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                    // пересечение по Y
                    else
                    {
                        _figures[i].ReverseDy(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].RightBorder - _figures[j].X,
                            Y = _figures[i].BottomBorder,
                            Type = _figures[j].GetType()
                        });
                    }
                }
                // левая середина
                else if (_figures[i].Y > _figures[j].Y && _figures[i].BottomBorder < _figures[j].BottomBorder)
                {
                    _figures[i].ReverseDx(_isRun[i]);
                    _figures[i].OnCross(this, new FigureEventArgs()
                    {
                        X = _figures[i].RightBorder,
                        Y = _figures[i].BottomBorder - _figures[i].Y,
                        Type = _figures[j].GetType()
                    });
                }
                // левый нижний угол
                else if (_figures[i].Y < _figures[j].BottomBorder && _figures[i].BottomBorder > _figures[j].BottomBorder)
                {
                    // пересечение по X
                    if (_figures[i].RightBorder - _figures[j].X < _figures[j].BottomBorder - _figures[i].Y)
                    {
                        _figures[i].ReverseDx(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].RightBorder,
                            Y = _figures[j].BottomBorder - _figures[i].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                    // пересечение по Y
                    else
                    {
                        _figures[i].ReverseDy(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].RightBorder - _figures[j].X,
                            Y = _figures[i].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                }
            }
            // пересечение справа на лево
            else if (_figures[i].X > _figures[j].X && _figures[i].X < _figures[j].RightBorder)
            {
                // правый верхний угол
                if (_figures[i].Y < _figures[j].Y && _figures[i].BottomBorder > _figures[j].Y)
                {
                    // пересечение по X
                    if (_figures[j].RightBorder - _figures[i].X < _figures[i].BottomBorder - _figures[j].Y)
                    {
                        _figures[i].ReverseDx(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].X,
                            Y = _figures[i].BottomBorder - _figures[j].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                    // пересечение по Y
                    else
                    {
                        _figures[i].ReverseDy(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[j].RightBorder - _figures[i].X,
                            Y = _figures[i].BottomBorder,
                            Type = _figures[j].GetType()
                        });
                    }
                }
                // правая середина 
                else if (_figures[i].Y > _figures[j].Y && _figures[i].BottomBorder < _figures[j].BottomBorder)
                {
                    _figures[i].ReverseDx(_isRun[i]);
                    _figures[i].OnCross(this, new FigureEventArgs()
                    {
                        X = _figures[i].X,
                        Y = _figures[i].BottomBorder - _figures[i].Y,
                        Type = _figures[j].GetType()
                    });
                }
                // правый нижний угол
                else if (_figures[i].Y < _figures[j].BottomBorder && _figures[i].BottomBorder > _figures[j].BottomBorder)
                {
                    // пересечение по X
                    if (_figures[j].RightBorder - _figures[i].X < _figures[j].BottomBorder - _figures[i].Y)
                    {
                        _figures[i].ReverseDx(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[i].X,
                            Y = _figures[j].BottomBorder - _figures[i].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                    // пересечеине по Y
                    else
                    {
                        _figures[i].ReverseDy(_isRun[i]);
                        _figures[i].OnCross(this, new FigureEventArgs()
                        {
                            X = _figures[j].RightBorder - _figures[i].X,
                            Y = _figures[i].Y,
                            Type = _figures[j].GetType()
                        });
                    }
                }
            }
        }

        //private List<Tuple<int, int, int, int>> CreateFreePlaces()
        //{
        //    List<Tuple<int, int, int, int>> freePlaces = new List<Tuple<int, int, int, int>>();

        //    Figure[] arrayOfFigures = new Figure[_figures.Count];
        //    _figures.CopyTo(arrayOfFigures, 0);

        //    List<Figure> sortFigures = arrayOfFigures.ToList();
        //    sortFigures.Sort((f1, f2) =>  f1.X.CompareTo(f2.X));

        //    int x = 0, rightBorder = 0;
        //    int y = 0, bottomBorder = 0;
        //    foreach (Figure figure in sortFigures)
        //    {
                
        //    }

        //    return freePlaces;
        //}
        
        private void btnTriangle_Click(object sender, EventArgs e)
        {
            var triangle = new Triangle(pbMain.Width, pbMain.Height);
            if (triangle.IsCoordinatesCorrect(_figures))
            {
                _figures.Add(triangle);
                _isRun.Add(true);
                listViewFigures.Items.Add("Triangle" + _figures.Count);
                listViewFigures.Items[listViewFigures.Items.Count - 1].ForeColor = 
                    Color.FromArgb(triangle.Color[0], triangle.Color[1], triangle.Color[2], triangle.Color[3]);
            }
        }
        private void btnCircle_Click(object sender, EventArgs e)
        {
            var circle = new Circle(pbMain.Width, pbMain.Height);
            if (circle.IsCoordinatesCorrect(_figures))
            {
                _figures.Add(circle);
                _isRun.Add(true);
                listViewFigures.Items.Add("Circle" + _figures.Count);
                listViewFigures.Items[listViewFigures.Items.Count - 1].ForeColor = 
                    Color.FromArgb(circle.Color[0], circle.Color[1], circle.Color[2], circle.Color[3]);
            }
        }
        private void btnRectangle_Click(object sender, EventArgs e)
        {
            var rectangle = new Rectangle(pbMain.Width, pbMain.Height);
            if (rectangle.IsCoordinatesCorrect(_figures))
            {
                _figures.Add(rectangle);
                _isRun.Add(true);
                listViewFigures.Items.Add("Rectangle" + _figures.Count);
                listViewFigures.Items[listViewFigures.Items.Count - 1].ForeColor = 
                    Color.FromArgb(rectangle.Color[0], rectangle.Color[1], rectangle.Color[2], rectangle.Color[3]);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            pbMain.Refresh();
        }

        private void btnStopRun_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewFigures.SelectedItems;
            
            if (selectedItems.Count > 0)
            {
                for (var i = 0; i < selectedItems.Count; i++)
                {
                    var index = listViewFigures.Items.IndexOf((selectedItems[i]));
                    if (_isRun[index])
                    {
                        _isRun[index] = false;
                        btnStopRun.Text = _resourceManager.GetString("btnStopRun.Text.Run");
                        listViewFigures.Items[index].Font = new Font(listViewFigures.Items[index].Font, FontStyle.Underline);
                    }
                    else
                    {
                        _isRun[index] = true;
                        btnStopRun.Text = _resourceManager.GetString("btnStopRun.Text.Stop");
                        listViewFigures.Items[index].Font = new Font(listViewFigures.Items[index].Font, FontStyle.Regular);
                    }
                }
            }
        }

        private void listViewFigures_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewFigures.SelectedItems;
            if (selectedItems.Count != 1)
            {
                MessageBox.Show("Выбран не один элемент.");
                return;
            }
            var index = listViewFigures.Items.IndexOf((selectedItems[0]));
            btnStopRun.Text = _isRun[index] ? _resourceManager.GetString("btnStopRun.Text.Stop") : _resourceManager.GetString("btnStopRun.Text.Run");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (DialogResult.Yes == MessageBox.Show(@"Вы действительно хотите закрыть программу? Все несохраненные данные будут потеряны", "", MessageBoxButtons.YesNo))
            //    e.Cancel = false;
            //else
            //    e.Cancel = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "Error";
            SaveFileDialog saveAsFileDialog = new SaveFileDialog
            {
                FileName = "figures",
                Filter = @"XML files(*.xml)|*.xml|JSON files(*.json)|*.json|Binary files(*.bin)|*.bin",
                InitialDirectory = "..//..//..//Saved_Figures"
            };
            if (DialogResult.OK == saveAsFileDialog.ShowDialog())
            {
                fileName = saveAsFileDialog.FileName;
                switch (saveAsFileDialog.FilterIndex)
                {
                    case 1:
                        var serializer = new XmlSerializer(typeof(List<Figure>));
                        serializer.Serialize(saveAsFileDialog.OpenFile(), _figures);
                        break;
                    case 2:
                        var jsonSerializer = new DataContractJsonSerializer(typeof(List<Figure>), 
                            new Type[] { typeof(Triangle), typeof(Circle), typeof(Rectangle) });
                        jsonSerializer.WriteObject(saveAsFileDialog.OpenFile(), _figures);
                        break;
                    case 3:
                        var formatter = new BinaryFormatter();
                        formatter.Serialize(saveAsFileDialog.OpenFile(), _figures);
                        break;
                    default:
                        MessageBox.Show("Data are not saved.");
                        return;
                }
            }
            MessageBox.Show(fileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = "Error";
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = @"XML files(*.xml)|*.xml|JSON files(*.json)|*.json|Binary files(*.bin)|*.bin",
                InitialDirectory = "..//..//..//Saved_Figures"
            };
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                fileName = openFileDialog.FileName;
                switch (openFileDialog.FilterIndex)
                {
                    case 1:
                        var serializer = new XmlSerializer(typeof(List<Figure>));
                        _figures = (List<Figure>) serializer.Deserialize(openFileDialog.OpenFile());
                        break;
                    case 2:
                        var jsonSerializer = new DataContractJsonSerializer(typeof(List<Figure>),
                            new Type[] { typeof(Triangle), typeof(Circle), typeof(Rectangle) });
                        _figures = (List<Figure>) jsonSerializer.ReadObject(openFileDialog.OpenFile());
                        break;
                    case 3:
                        var formatter = new BinaryFormatter();
                        _figures = (List<Figure>) formatter.Deserialize(openFileDialog.OpenFile());
                        break;
                    default:
                        MessageBox.Show("Invalid file format.");
                        return;
                }
            }
            if(_isRun.Count != _figures.Count)
                _isRun = new bool[_figures.Count].ToList();
            MessageBox.Show(fileName);
        }

        private void cbLocalization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLocalization.SelectedIndex == 0)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(this, "$this");
                applyResources(resources, this.Controls);
            }
            else if (cbLocalization.SelectedIndex == 1)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(this, "$this");
                applyResources(resources, this.Controls);
            }
            else
                MessageBox.Show("Invalid localization");

            RenameMenu();
        }

        private void applyResources(ComponentResourceManager resources, Control.ControlCollection ctls)
        {
            foreach (Control ctl in ctls)
            {
                resources.ApplyResources(ctl, ctl.Name);
                applyResources(resources, ctl.Controls);
            }
        }

        private void checkInterfaceColizionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var circle1 = new Circle();
            IMovable movable =  circle1;
            IDrawable drawable = circle1;
            movable.Foo();
            drawable.Foo();
        }

        private void RenameMenu()
        {
            fileToolStripMenuItem.Text = _resourceManager.GetString("fileToolStripMenuItem.Text");
            openToolStripMenuItem.Text = _resourceManager.GetString("openToolStripMenuItem.Text");
            saveAsToolStripMenuItem.Text = _resourceManager.GetString("saveAsToolStripMenuItem.Text");
            exitToolStripMenuItem.Text = _resourceManager.GetString("exitToolStripMenuItem.Text");
            checkInterfacesColizionToolStripMenuItem.Text =
                _resourceManager.GetString("checkInterfacesColizionToolStripMenuItem.Text");
        }

        private void checkMyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //List<int> l = new List<int>();
            //l.Add(4);
            //l.Add(13);
            //l.RemoveAt(0);
            
            MyList<int> list = new MyList<int>();
            foreach (int number in Enumerable.Range(101, 10))
            {
                list.Add(number);
            }
            string result = "First iteration\r\n";
            foreach (var item in list)
            {
                result += item + "\r\n";
            }

            //Tests
            //list.Insert(0, 12);
            //list.Insert(4, 87);
            //list.Insert(10, 999);
            //result += "Second iteration\r\n";
            //foreach (var item in list)
            //{
            //    result += item + "\r\n";
            //}


            MessageBox.Show(result);
            Close();
            //Debug.WriteLine(result);
        }

        private void bntAddEvent_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewFigures.SelectedItems;

            if (selectedItems.Count > 0)
            {
                for (var i = 0; i < selectedItems.Count; i++)
                {
                    var index = listViewFigures.Items.IndexOf((selectedItems[i]));
                    _figures[index].AddBeep();
                }
            }
        }

        private void btnRemoveEvent_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewFigures.SelectedItems;

            if (selectedItems.Count > 0)
            {
                for (var i = 0; i < selectedItems.Count; i++)
                {
                    var index = listViewFigures.Items.IndexOf((selectedItems[i]));
                    _figures[index].RemoveBeep();
                }
            }
        }
    }
}

/*
                        //if (i == j)
                        //    continue;
                        //if ((_figures[i].X <= _figures[j].X && _figures[i].RightBorder >= _figures[j].X)
                        //        //&& _figures[i].RightBorder <= _figures[j].RightBorder)
                        //    || (_figures[i].X >= _figures[j].X && _figures[i].X <= _figures[j].RightBorder 
                        //        && _figures[i].RightBorder >= _figures[j].RightBorder))
                        //{
                        //    if ((_figures[i].Y <= _figures[j].Y && _figures[i].BottomBorder >= _figures[j].Y)
                        //        || (_figures[i].Y >= _figures[j].Y && _figures[i].Y <= _figures[j].BottomBorder))
                        //    {
                        //        //if(_figures[i].Dx)
                        //        _figures[i].ReverseDx(_isRun[i]);
                        //    }
                        //}
                        //else if (_figures[i].X >= _figures[j].X && _figures[i].RightBorder <= _figures[j].RightBorder)
                        //{
                        //    if ((_figures[i].Y <= _figures[j].Y && _figures[i].BottomBorder >= _figures[j].Y)
                        //        || (_figures[i].Y >= _figures[j].Y && _figures[i].Y <= _figures[j].BottomBorder))
                        //    {
                        //        _figures[i].ReverseDy(_isRun[i]);
                        //    }
                        //}*/
