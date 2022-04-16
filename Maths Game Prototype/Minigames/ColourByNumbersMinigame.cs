using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace Maths_Game_Prototype.Minigames
{
    internal class ColourByNumbersMinigame : Minigame
    {
        private ColourableShape[] _colourableShapes; //Holds all shapes in the caterpillar image and their associated correct colours.
        //private PaintPot[] PaintPots; //Holds all buttons on the right-hand side of the game for the user to fill their bucket with
        public Dictionary<Border, String> PaintPots { get; set; } //A dictionary that holds all paint pots and part of the file name for their associated bucket cursors.
        public SolidColorBrush SelectedColour { get; set; } //Holds the colour the user has in their bucket.

        /// <summary>
        /// Constructor 
        /// </summary>
        public ColourByNumbersMinigame()
        {
            GameName = "Colour by Numbers";
            Tooltip = "Click the numbers on the right to fill your bucket, then paint onto the picture.";
        }

        /// <summary>
        /// Initalises all shapes and paint pots, and expected colours of the shapes.
        /// Sets all shapes in the picture to white for the user to colour in.
        /// Resets all variables from previous playthroughs of the minigame
        /// Sets up UI for the game.
        /// </summary>
        public override void NewGame()
        {
            base.NewGame();

            _colourableShapes = new[] //Populates array with shapes with their associated UI elements and expected colours
            {
                new ColourableShape(MainWindow.Face, new SolidColorBrush(Color.FromRgb(0xD9, 0x5D, 0x5D))),
                new ColourableShape(MainWindow.Ball1, new SolidColorBrush(Color.FromRgb(0xF1, 0xB0, 0x4E))),
                new ColourableShape(MainWindow.Ball2, new SolidColorBrush(Color.FromRgb(0xE5, 0xE8, 0x4E))),
                new ColourableShape(MainWindow.Ball3, new SolidColorBrush(Color.FromRgb(0x8D, 0xDC, 0x80))),
                new ColourableShape(MainWindow.Ball4, new SolidColorBrush(Color.FromRgb(0x6A, 0xC3, 0xDF))),
                new ColourableShape(MainWindow.Grass, new SolidColorBrush(Color.FromRgb(0x00, 0x6F, 0x10))),
                new ColourableShape(MainWindow.Sky, new SolidColorBrush(Color.FromRgb(0x05, 0xE0, 0xFF))),
                new ColourableShape(MainWindow.Sun, new SolidColorBrush(Color.FromRgb(0xFF, 0xFD, 0x00)))
            };

            PaintPots = new Dictionary<Border, string> //Populates array with paint pots with their associated UI elements and part of their paths for their associated bucket cursors.
            {
                { MainWindow.PaintPot1, "face" },
                { MainWindow.PaintPot2, "sun" },
                { MainWindow.PaintPot3, "ball3" },
                { MainWindow.PaintPot4, "sky" },
                { MainWindow.PaintPot5, "ball1" },
                { MainWindow.PaintPot6, "ball2" },
                { MainWindow.PaintPot7, "grass" },
                { MainWindow.PaintPot8, "ball4" }
            };

            SelectedColour = null; //Ensures there is no selected colour when game starts

            foreach (var shape in _colourableShapes) //Turns all colourable shapes of the picture to white.
            {
                shape.Shape.Fill = new SolidColorBrush(Colors.White);
            }

            ShowMinigameLayout(MainWindow.ColourByNumbersGrid); //Sets up UI for the game

            MainWindow.CateBackground.Cursor = Cursors.Arrow; //Resets the cursor when hovered the picture back to an arrow.

            foreach (var paintPot in PaintPots) //Makes cursor turn into a colour picker icon when hovered over a paint pot.
            {
                paintPot.Key.Cursor =
                    new Cursor(Application.GetResourceStream(new Uri("colour-picker.cur", UriKind.Relative)).Stream);
            }

            LockGame(false); //Allows the user to freely interact with the UI.
        }

        /// <summary>
        /// Checks if all shapes in the picture match their associated correct colours.
        /// If they do, a message is displayed to the user congratulating them, and are prompted to finish.
        /// All UI elements in the game lose interactivity if all shapes match their correct colours.
        /// </summary>
        public void CheckColours()
        {
            if (_colourableShapes.Any(shape => shape.CurrentColour.Color != shape.ExpectedColour.Color)) return; //Ends this procedure if there are shapes in the picture that don't match their expected colour

            LockGame(true); //Prevents user from interacting with UI

            MainWindow.AnswerRevealArea.Visibility = Visibility.Visible; // Displays encouraging message
            MainWindow.AnswerRevealText.Text = "Well done. You did it."; //
            MainWindow.PlaySound(Sounds.thrilled); // Plays encouraging sound
            MainWindow.NextQBtn.Content = new TextBlock {Text = "Finish"}; // Prompts user to leave
        }

        protected override void LockGame(bool locked)
        {
            MainWindow.CateBackground.IsEnabled = !locked; //Disables user from interacting with the picture & filling it with colours.

            foreach (var paintPot in PaintPots) //Disables the user from filling their bucket using the paint pots at the side.
                paintPot.Key.IsEnabled = !locked;
        }
    }
}
