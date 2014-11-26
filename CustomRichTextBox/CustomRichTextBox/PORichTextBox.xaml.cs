using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CustomRichTextBox.UserControls
{
    /// <summary>
    /// Interaction logic for PODRichTextBox.xaml
    /// </summary>
    public partial class PORichTextBox : UserControl
    {
        public PORichTextBox()
        {
            InitializeComponent();


        }





        public bool IsEditMode
        {
            get { return (bool)GetValue(IsEditModeProperty); }
            set { SetValue(IsEditModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsEditMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEditModeProperty =
            DependencyProperty.Register("IsEditMode", typeof(bool), typeof(PORichTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(UpdateProperty_Changed)));





        public int RTHeight
        {
            get { return (int)GetValue(RTHeightProperty); }
            set { SetValue(RTHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RTHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RTHeightProperty =
            DependencyProperty.Register("RTHeight", typeof(int), typeof(PORichTextBox), new PropertyMetadata(0));

        
        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchTextProperty =
            DependencyProperty.Register("SearchText", typeof(string), typeof(PORichTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(UpdateProperty_Changed)));



        private static void UpdateProperty_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PORichTextBox textBlock = (PORichTextBox)d;
            textBlock.RenderHighlightedText();
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(String), typeof(PORichTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(UpdateProperty_Changed)));



        public void Clear()
        {
            rtDocument.Blocks.Clear();


        }




        public bool IsTitleEnable
        {
            get { return (bool)GetValue(IsTitleEnableProperty); }
            set { SetValue(IsTitleEnableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsTitleEnable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsTitleEnableProperty =
            DependencyProperty.Register("IsTitleEnable", typeof(bool), typeof(PORichTextBox), new PropertyMetadata(false));


        public bool IsRedColorForReadOnly
        {
            get { return (bool)GetValue(IsRedColorForReadOnlyProperty); }
            set { SetValue(IsRedColorForReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsNegativeResponseInjuredNote.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsRedColorForReadOnlyProperty =
            DependencyProperty.Register("IsRedColorForReadOnly", typeof(bool), typeof(PORichTextBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(UpdateProperty_Changed)));




        public int NumberOfReadOnlyCharacter
        {
            get { return (int)GetValue(NumberOfReadOnlyCharacterProperty); }
            set { SetValue(NumberOfReadOnlyCharacterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NumberOfReadOnlyCharacter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberOfReadOnlyCharacterProperty =
            DependencyProperty.Register("NumberOfReadOnlyCharacter", typeof(int), typeof(PORichTextBox), new PropertyMetadata(0));



        TextPointer _previsousCaretPointer;

        private Run GetForegroundColor(string strInformation, Brush color)
        {
            Run noramlRun = new Run(strInformation);
            noramlRun.Foreground = color;
            return noramlRun;
        }

        private Run GetDefaultColor(string strInformation)
        {
            return GetForegroundColor(strInformation, this.Foreground);

        }

        private Run GetGrayColor(string strInformation)
        {
            return GetForegroundColor(strInformation, Brushes.LightGray);
        }

        private Run GetBlackColor(string strInformation)
        {
            return GetForegroundColor(strInformation, Brushes.Black);
        }

        private Run GetGreenColor(string strInformation)
        {
            return GetForegroundColor(strInformation, Brushes.Green);
        }

        private Run GetWhileColor(string strInformation)
        {
            return GetForegroundColor(strInformation, Brushes.White);
        }
        private Run GetRedColor(string strInformation)
        {
            return GetForegroundColor(strInformation, Brushes.Red);
        }



        public void RenderHighlightedText()
        {
            if (rtDocument.Blocks != null)
            {
                rtDocument.Blocks.Clear();
                _readOnlyText = null;
                if (Text != null)
                {
                 
                        DoHighlightDescription(Text);
                    

                }

            }

        }


        private void DoHighlightDescription(string strInput)
        {
            PODRichTxtBox.FontSize = 16;
            PODRichTxtBox.FontWeight = FontWeights.Normal;
            Paragraph para = txtParagraph;

            para.Inlines.Clear();

            _previsousCaretPointer = null;
            if (string.IsNullOrEmpty(strInput))
            {
                if (IsEditMode)
                {
                    para.Foreground = Brushes.Green;
                    rtDocument.Blocks.Add(para);
                }

            }

            if (!string.IsNullOrEmpty(strInput))
            {

                if (string.IsNullOrEmpty(SearchText) || IsEditMode)
                {
                    if (NumberOfReadOnlyCharacter > 0 && strInput.Length >= NumberOfReadOnlyCharacter)
                    {

                        string strReadOnly = strInput.Substring(0, NumberOfReadOnlyCharacter);
                        _readOnlyText = strReadOnly;

                        Run textBlock = GetBlackColor(strReadOnly);

                
                        if (IsRedColorForReadOnly)
                        {
                            textBlock.Foreground = Brushes.Red;
                        }
                        else
                        {
                            textBlock.Foreground = Brushes.Black;
                        }

                     

                        para.Inlines.Add(textBlock);

                        string remainingText = null;
                        if (NumberOfReadOnlyCharacter <= strInput.Length)
                        {
                            remainingText = strInput.Substring(NumberOfReadOnlyCharacter, (strInput.Length - NumberOfReadOnlyCharacter));

                        }
                        else
                        {
                            remainingText = "";
                        }


                        if (string.IsNullOrEmpty(remainingText))
                        {
                            remainingText = " ";
                        }
                            //string remainingText = strInput.Substring(NumberOfReadOnlyCharacter, (strInput.Length - NumberOfReadOnlyCharacter));

                            if (IsEditMode)
                            {
                                para.Inlines.Add(GetGreenColor(remainingText));
                            }
                            else
                            {
                                para.Inlines.Add(GetBlackColor(remainingText));
                            }

                        
                    }
                    else if (IsEditMode)
                    {
                        para.Inlines.Add(GetGreenColor(strInput));

                    }
                    else
                    {
                        para.Inlines.Add(GetBlackColor(strInput));
                    }


                }
                else if (!string.IsNullOrEmpty(SearchText) && !IsEditMode)
                {
                    //Search Text
                    var lowerInput = strInput.ToLower();
                    var lowerSearchText = SearchText.ToLower();
                    if (lowerInput.Contains(lowerSearchText))
                    {
                        int startPoint = lowerInput.IndexOf(lowerSearchText);

                        Run boldText = GetBlackColor(strInput.Substring(startPoint, SearchText.Length));

                        if (startPoint == 0)
                        {
                            para.Inlines.Add(boldText);
                            //Get remaining text

                            int remainingLength = strInput.Length - (startPoint + SearchText.Length);
                            string remaingText = strInput.Substring((startPoint + SearchText.Length), remainingLength);

                            para.Inlines.Add(GetGrayColor(remaingText));
                        }
                        else
                        {

                            string _originalText = strInput;
                            string firstPart = _originalText.Substring(0, startPoint);

                            para.Inlines.Add(GetGrayColor(firstPart));

                            para.Inlines.Add(boldText);

                            int remainingLength = _originalText.Length - (startPoint + SearchText.Length);
                            string remaingText = _originalText.Substring((startPoint + SearchText.Length), remainingLength);

                            para.Inlines.Add(GetGrayColor(remaingText));
                        }


                    }
                    else
                    {
                        para.Inlines.Add(GetGrayColor(strInput));
                    }


                }
                rtDocument.Blocks.Add(para);

            }
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Back
            || e.Key == Key.Delete
            || e.Key == Key.Left)
            {

                if (NumberOfReadOnlyCharacter > 0)
                {

                    var startPoint = PODRichTxtBox.CaretPosition.DocumentStart;
                    var endPoint = PODRichTxtBox.CaretPosition;
                    TextRange textRange = new TextRange(startPoint, endPoint);
                    int lenght = textRange.Text.Length;             
                    var updatedString = GetText(PODRichTxtBox);

                    if (lenght <= NumberOfReadOnlyCharacter+1)
                    {
                        e.Handled = true;
                        return;
                    }

                }

            }


            base.OnPreviewKeyDown(e);
        }

        string GetText(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(rtb.Document.ContentStart,
                rtb.Document.ContentEnd);

            var editableText = textRange.Text;
            editableText = editableText.Replace("\r\n", "");
          
            return editableText;
        }





        public string _readOnlyText { get; set; }

        private void RichTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (IsEditMode)
            {
                RichTextBox richTextBox = (RichTextBox)sender;
                if (richTextBox != null)
                {
                    Text = GetText(richTextBox);
                }

            }


        }

        public object CaretIndex { get; set; }

        private void PODRichTxtBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
         
            
            RichTextBox rtbEditor = (RichTextBox)sender;
            var startPoint = rtbEditor.CaretPosition.DocumentStart;
            var endPoint = rtbEditor.CaretPosition;
            TextRange textRange = new TextRange(startPoint, endPoint);
            int lenght = textRange.Text.Length;

            var updatedString = GetText(rtbEditor);

            if (lenght < NumberOfReadOnlyCharacter)
            {

                if (_previsousCaretPointer == null)
                {
                    rtbEditor.CaretPosition = rtbEditor.Document.ContentEnd;
                }
                else
                {
                    rtbEditor.CaretPosition = _previsousCaretPointer;
                }
            
            }


            _previsousCaretPointer = rtbEditor.CaretPosition;


        }

      



    }
}
