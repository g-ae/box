using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace boxapp
{
    public partial class MainWindow : Window
    {
        // nbre maximal lignes et colonnes = 10 (sinon algorithme avec noms ne fonctionne pas => dernier label serait p.ex.: game127)
        int nbre_colonnes = 10;
        int nbre_lignes = 7;        
        Label[,] lstLbl;
        List<Label> lstHTP;
        bool isSpacePressed = false;
        Random r = new Random();
        Settings settings = new Settings();
        public MainWindow()
        {
            InitializeComponent();
            InitializeList();
        }
        private void InitializeList()
        {
            lstLbl = new Label[nbre_lignes, nbre_colonnes];
            lstHTP = new List<Label>();
            lstHTP.Add(new Label()
            {
                Name = "htpTitle",
                Content = "how to play",
                Height = 48,
                Width = 160,
                Margin = new Thickness(10, 10, 0, 0),
                FontSize = 20
            });
            lstHTP.Add(new Label()
            {
                Name = "htpThisIsYou",
                Content = "This is you :",
                Height = 41,
                Width = 76,
                Margin = new Thickness(10, 58, 0, 0)
            });
            lstHTP.Add(new Label()
            {
                Name = "htpPlayer",
                Height = 30,
                Width = 30,
                Margin = new Thickness(111, 63, 0, 0),
                Background = Brushes.Cyan
            });
            lstHTP.Add(new Label()
            {
                Name = "htpEnd",
                Height = 30,
                Width = 30,
                Margin = new Thickness(346, 100, 0, 0),
                Background = Brushes.Lime
            });
            lstHTP.Add(new Label()
            {
                Name = "htpBox",
                Height = 30,
                Width = 30,
                Margin = new Thickness(231, 100, 0, 0),
                Background = Brushes.SandyBrown
            });
            lstHTP.Add(new Label()
            {
                Name = "htpObjective",
                Content = "your objective is to push all the boxes (            ) into the end (           )",
                Height = 47,
                Width = 380,
                Margin = new Thickness(10, 99, 0, 0)
            });
            lstHTP.Add(new Label()
            {
                Name = "htpHowToMove",
                Content = "you can move by using your arrow keys.",
                Height = 31,
                Width = 228,
                Margin = new Thickness(10, 146, 0, 0)
            });
            lstHTP.Add(new Label()
            {
                Name = "htpExampleMove",
                Content = "to move a box to the right, you have to get it like this :",
                Height = 31,
                Width = 300,
                Margin = new Thickness(10, 206, 0, 0)
            });
            lstHTP.Add(new Label()
            {
                Name = "htpExamplePlayer",
                Height = 30,
                Width = 30,
                Margin = new Thickness(326, 206, 0, 0),
                Background = Brushes.Cyan
            });
            lstHTP.Add(new Label()
            {
                Name = "htpExampleBox",
                Height = 30,
                Width = 30,
                Margin = new Thickness(361, 206, 0, 0),
                Background = Brushes.SandyBrown
            });
            lstHTP.Add(new Label()
            {
                Name = "htpExampleEnd",
                Height = 30,
                Width = 30,
                Margin = new Thickness(396, 206, 0, 0),
                Background = Brushes.Lime
            });
            lstHTP.Add(new Label()
            {
                Name = "htpExampleMoveKey",
                Content = "and then you just press your right arrow and the box will go inside the end.",
                Height = 31,
                Width = 404,
                Margin = new Thickness(10, 242, 0, 0)
            });
            lstHTP.Add(new Label()
            {
                Name = "htpSpace",
                Content = "you can hold spacebar to pull the box with you",
                Height = 30,
                Width = 268,
                Margin = new Thickness(6, 292, 0, 0)
            });
            Label backToMenu = new Label()
            {
                Name = "htpBackToMenu",
                Content = "back to the menu",
                Height = 48,
                Width = 171,
                Margin = new Thickness(396, 363, 0, 0),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            backToMenu.MouseUp += menu_MouseUp;
            lstHTP.Add(backToMenu);

            foreach (Label l in lstHTP)
            {
                l.HorizontalAlignment = HorizontalAlignment.Left;
                l.VerticalAlignment = VerticalAlignment.Top;
                l.Foreground = Brushes.White;
                l.Visibility = Visibility.Hidden;
                l.HorizontalContentAlignment = HorizontalAlignment.Center;
                l.VerticalContentAlignment = VerticalAlignment.Center;
                mainGrid.Children.Add(l);
            }
        }
        private void menu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (sender.GetType().Equals(new Label().GetType()))
            {
                Label sent = (Label)sender;
                switch (sent.Name)
                {
                    case "menuStart":
                        StartGame();
                        break;
                    case "menuHowToPlay":
                        MenuToHidden();
                        InfoHowToPlay();
                        break;
                    case "menuSettings":
                        MenuToHidden();
                        ShowSettings();
                        break;
                    case "lblRestart":
                        Restart();
                        break;
                    case "htpBackToMenu":
                        HowToPlayToHidden();
                        BackToMenu();
                        break;
                }
            }
        }
        private void ShowSettings()
        {

        }
        private void Restart()
        {
            DeleteExistingGameLabels();
            StartGame();
        }
        private void DeleteExistingGameLabels()
        {
            bool continuer;
            do
            {
                try
                {
                    Label lbl = mainGrid.Children.Cast<Label>().First(l => l.Name.StartsWith("game"));
                    mainGrid.Children.Remove(lbl);
                    continuer = true;
                }
                catch (Exception)
                {
                    continuer = false;
                }
            }
            while (continuer);
        }
        private void StartGame()
        {
            if (nbre_lignes > 10 || nbre_colonnes > 10)
            {
                MessageBox.Show("Le nombre de lignes ou colonnes a dépassé la limite (10)", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MenuToHidden();
                InitializeList();
                CreateInGameLabels();
                lblRestart.Visibility = Visibility.Visible;
            }
        }
        private void InfoHowToPlay()
        {
            foreach(Label l in lstHTP)
            {
                mainGrid.Children[mainGrid.Children.IndexOf(l)].Visibility = Visibility.Visible;
            }
        }
        private void MenuToHidden()
        {
            foreach (UIElement el in mainGrid.Children)
            {
                if (el.GetType() == new Label().GetType())
                {
                    Label lbl = (Label)el;
                    if (lbl.Name.StartsWith("menu"))
                    {
                        lbl.Visibility = Visibility.Hidden;
                    }
                }
            }
        }
        private void HowToPlayToHidden()
        {
            foreach (Label l in lstHTP)
            {
                mainGrid.Children[mainGrid.Children.IndexOf(l)].Visibility = Visibility.Hidden;
            }
        }
        private void CreateInGameLabels()
        {
            for (int l = 0; l <= nbre_lignes - 1; l++)
            {
                if (l != 0 && l != nbre_lignes - 1)
                {
                    for (int c = 0; c <= nbre_colonnes - 1; c++)
                    {
                        if (c != 0 && c != nbre_colonnes - 1)
                        {
                            //lstLbl[l, c] = new Label() { Name = $"game{l}{c}" };
                            CreateLabel(l, c, Status.CaseType.Background);
                        }
                        else
                        {
                            //lstLbl[l, c] = new Label() { Name = $"game{l}{c}" };
                            CreateLabel(l, c, Status.CaseType.Frontier);
                        }
                    }
                }
                else
                {
                    for (int c = 0; c <= nbre_colonnes - 1; c++)
                    {
                        CreateLabel(l, c, Status.CaseType.Frontier);
                    }
                }
            }

            // TODO À AMELIORER

            GenerateLabel(Status.CaseType.Player);
            GenerateLabel(Status.CaseType.Box);
            GenerateLabel(Status.CaseType.Box);
            GenerateLabel(Status.CaseType.End);
        }
        private Label CreateLabel(int l, int c, string casetype)
        {
            Label createdLabel = new Label()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Width = 30,
                Height = 30,
                Tag = casetype,
                Name = $"game{l}{c}"
            };
            createdLabel.Margin = new Thickness(10 + (c * (createdLabel.Width + 2)), 10 + (l * (createdLabel.Height + 2)), 0, 0);
            switch (casetype)
            {
                case Status.CaseType.Frontier:
                    createdLabel.Background = Brushes.Gray;
                    break;
                case Status.CaseType.Background:
                    createdLabel.Background = Brushes.White;
                    break;
                case Status.CaseType.Box:
                    createdLabel.Background = Brushes.SandyBrown;
                    break;
                case Status.CaseType.Player:
                    createdLabel.Background = Brushes.Aqua;
                    break;
                case Status.CaseType.End:
                    createdLabel.Background = Brushes.Lime;
                    break;
            }
            if (mainGrid.Children.Contains(lstLbl[l, c]))
            {
                mainGrid.Children.Remove(lstLbl[l, c]);
            }
            mainGrid.Children.Insert(Convert.ToInt32(String.Concat(l, c)), createdLabel);
            lstLbl[l, c] = createdLabel;
            return createdLabel;
        }
        private Label GetPlayer()
        {
            foreach (UIElement el in mainGrid.Children)
            {
                if (el.GetType() == new Label().GetType())
                {
                    Label lbl = (Label)el;
                    if ((string)lbl.Tag == Status.CaseType.Player)
                    {
                        return lbl;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Calcule la position du label fourni en vérifiant le numéro dans le nom.
        /// </summary>
        /// <param name="lbl">Label à fournir</param>
        /// <returns>Retourne la position (en string) du label envoyé sous le format: LC (par exemple, pour ligne 2, colonne 3: 23)</returns>
        private string GetPos(Label lbl)
        {
            int l, c;
            l = Convert.ToInt32(lbl.Name[4].ToString());
            c = Convert.ToInt32(lbl.Name[5].ToString());
            return string.Concat(l, c);
        }
        private Label GetLabel(int l, int c)
        {
            return mainGrid.Children.Cast<Label>().First(label => label.Name == $"game{l}{c}");
        }
        private void Movement(string movementtype)
        {
            string posActuelle = GetPos(GetPlayer());
            int l = Convert.ToInt32(posActuelle[0].ToString());
            int c = Convert.ToInt32(posActuelle[1].ToString());
            switch (movementtype)
            {
                case Status.MovementType.Up:
                    switch (GetLabel(l - 1, c).Tag)
                    {
                        case Status.CaseType.Background:
                            if ((string)lstLbl[l + 1, c].Tag == Status.CaseType.Box && isSpacePressed)
                            {
                                CreateLabel(l - 1, c, Status.CaseType.Player);
                                CreateLabel(l, c, Status.CaseType.Box);
                                CreateLabel(l + 1, c, Status.CaseType.Background);
                                break;
                            }
                            CreateLabel(l - 1, c, Status.CaseType.Player);
                            CreateLabel(l, c, Status.CaseType.Background);
                            break;
                        case Status.CaseType.Box:
                            switch (lstLbl[l - 2, c].Tag)
                            {
                                case Status.CaseType.Background:
                                    CreateLabel(l - 2, c, Status.CaseType.Box);
                                    CreateLabel(l - 1, c, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    break;
                                case Status.CaseType.End:
                                    CreateLabel(l - 1, c, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    CheckEnd();
                                    break;
                            }
                            break;
                    }
                    break;
                case Status.MovementType.Down:
                    switch (GetLabel(l + 1, c).Tag)
                    {
                        case Status.CaseType.Background:
                            if ((string)lstLbl[l - 1, c].Tag == Status.CaseType.Box && isSpacePressed)
                            {
                                CreateLabel(l + 1, c, Status.CaseType.Player);
                                CreateLabel(l, c, Status.CaseType.Box);
                                CreateLabel(l - 1, c, Status.CaseType.Background);
                                break;
                            }
                            CreateLabel(l + 1, c, Status.CaseType.Player);
                            CreateLabel(l, c, Status.CaseType.Background);
                            break;
                        case Status.CaseType.Box:
                            switch (lstLbl[l + 2, c].Tag)
                            {
                                case Status.CaseType.Background:
                                    CreateLabel(l + 2, c, Status.CaseType.Box);
                                    CreateLabel(l + 1, c, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    break;
                                case Status.CaseType.End:
                                    CreateLabel(l + 1, c, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    CheckEnd();
                                    break;
                            }
                            break;
                    }
                    break;
                case Status.MovementType.Right:
                    switch (GetLabel(l, c + 1).Tag)
                    {
                        case Status.CaseType.Background:
                            if ((string)lstLbl[l, c - 1].Tag == Status.CaseType.Box && isSpacePressed)
                            {
                                CreateLabel(l, c + 1, Status.CaseType.Player);
                                CreateLabel(l, c, Status.CaseType.Box);
                                CreateLabel(l, c - 1, Status.CaseType.Background);
                                break;
                            }
                            CreateLabel(l, c + 1, Status.CaseType.Player);
                            CreateLabel(l, c, Status.CaseType.Background);
                            break;
                        case Status.CaseType.Box:
                            switch (lstLbl[l, c + 2].Tag)
                            {
                                case Status.CaseType.Background:
                                    CreateLabel(l, c + 2, Status.CaseType.Box);
                                    CreateLabel(l, c + 1, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    break;
                                case Status.CaseType.End:
                                    CreateLabel(l, c + 1, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    CheckEnd();
                                    break;
                            }
                            break;
                    }
                    break;
                case Status.MovementType.Left:
                    switch (GetLabel(l, c - 1).Tag)
                    {
                        case Status.CaseType.Background:
                            if ((string)lstLbl[l, c + 1].Tag == Status.CaseType.Box && isSpacePressed)
                            {
                                CreateLabel(l, c - 1, Status.CaseType.Player);
                                CreateLabel(l, c, Status.CaseType.Box);
                                CreateLabel(l, c + 1, Status.CaseType.Background);
                                break;
                            }
                            CreateLabel(l, c - 1, Status.CaseType.Player);
                            CreateLabel(l, c, Status.CaseType.Background);
                            break;
                        case Status.CaseType.Box:
                            switch (lstLbl[l, c - 2].Tag)
                            {
                                case Status.CaseType.Background:
                                    CreateLabel(l, c - 2, Status.CaseType.Box);
                                    CreateLabel(l, c - 1, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    break;
                                case Status.CaseType.End:
                                    CreateLabel(l, c - 1, Status.CaseType.Player);
                                    CreateLabel(l, c, Status.CaseType.Background);
                                    CheckEnd();
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    isSpacePressed = true;
                    break;
                case Key.Up:
                    Movement(Status.MovementType.Up);
                    break;
                case Key.Down:
                    Movement(Status.MovementType.Down);
                    break;
                case Key.Right:
                    Movement(Status.MovementType.Right);
                    break;
                case Key.Left:
                    Movement(Status.MovementType.Left);
                    break;
            }
        }
        private void CheckEnd()
        {
            bool end = false;
            try
            {
                Label lbl = mainGrid.Children.Cast<Label>().First(l => (string)l.Tag == Status.CaseType.Box);
            }
            catch (Exception)
            {
                end = true;
            }

            if (end)
            {
                MessageBox.Show("Well played !\nAll the boxes have been put in their place.", "End of the game", MessageBoxButton.OK, MessageBoxImage.Information);
                var res = MessageBox.Show("Do you want to restart ?", "Keep playing", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (res == MessageBoxResult.Yes)
                {
                    Restart();
                }
                else
                {
                    BackToMenu();
                }
            }
        }
        private void BackToMenu()
        {
            foreach (UIElement el in mainGrid.Children)
            {
                if (el.GetType() == new Label().GetType())
                {
                    Label lbl = (Label)el;
                    if (lbl.Name.StartsWith("game") || lbl.Name.StartsWith("htp"))
                    {
                        el.Visibility = Visibility.Hidden;
                    }
                    if (lbl.Name.StartsWith("menu"))
                    {
                        lbl.Visibility = Visibility.Visible;
                    }
                }
            }
            lblRestart.Visibility = Visibility.Hidden;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                isSpacePressed = false;
            }
        }
        private void GenerateLabel(string labelType)
        {
            int genC = r.Next(1, nbre_colonnes);
            int genL = r.Next(1, nbre_lignes);
            if (labelType != Status.CaseType.Background && labelType != Status.CaseType.Frontier)
            {
                if (labelType == Status.CaseType.End)
                {
                    try
                    {
                        Label lbl = mainGrid.Children.Cast<Label>().First(l => (string)l.Tag == Status.CaseType.End);
                    }
                    catch (Exception)
                    {
                        if ((string)lstLbl[genL, genC].Tag == Status.CaseType.Background)
                        {
                            CreateLabel(genL, genC, Status.CaseType.End);
                        }
                        else
                        {
                            GenerateLabel(labelType);
                        }
                    }
                }
                else if (labelType == Status.CaseType.Player)
                {
                    try
                    {
                        Label lbl = mainGrid.Children.Cast<Label>().First(l => (string)l.Tag == Status.CaseType.Player);
                    }
                    catch (Exception)
                    {
                        if ((string)lstLbl[genL, genC].Tag == Status.CaseType.Background)
                        {
                            CreateLabel(genL, genC, Status.CaseType.Player);
                        }
                        else
                        {
                            GenerateLabel(labelType);
                        }
                    }
                }
                else if (labelType == Status.CaseType.Box)
                {
                    if ((string)lstLbl[genL, genC].Tag == Status.CaseType.Background)
                    {
                        CreateLabel(genL, genC, Status.CaseType.Box);
                    }
                    else
                    {
                        GenerateLabel(labelType);
                    }
                }
            }
        }
    }
}