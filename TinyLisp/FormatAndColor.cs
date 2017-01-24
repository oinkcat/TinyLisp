using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TinyLisp
{
    public partial class frmMain
    {
        Font NormalStyle;
        Font BoldStyle;

        char[] Terminals;
        string[] MathSymbols;
        bool WordColorizingEnabled;

        int CommentStartLine = -1;

        private void InitializeColorizer()
        {
            NormalStyle = rtbSource.SelectionFont;
            BoldStyle = new Font(NormalStyle, FontStyle.Bold);
            Terminals = new char[] { '(', ')', ' ', '\n', ';' };
            MathSymbols = new string[] { "+", "-", "*", "/", "--", "=", "<", ">", "<=", ">=" };
            WordColorizingEnabled = true;
        }

        private void ColorizeWord(string word, int charPosition)
        {
            bool boldFont = false;
            Color wordColor = Color.Black;
            int lineIndex = rtbSource.GetLineFromCharIndex(charPosition);
            if (Array.IndexOf(Parser.syntax, word) > -1)
            {
                boldFont = true;
                wordColor = Color.Blue;
            }
            else if (Array.IndexOf(MathSymbols, word) > -1)
            {
                wordColor = Color.Red;
            }
            else if (word.StartsWith("\"") && word.EndsWith("\"") && word.Length > 1)
            {
                wordColor = Color.Green;
            }
            else if (word == "(" || word == ")")
            {
                wordColor = Color.Indigo;
            }
            else if (word == ";")
            {
                wordColor = Color.Gray;
                CommentStartLine = lineIndex;
            }
            else if (word == "'")
            {
                wordColor = Color.Blue;
                boldFont = true;
            }
            else
            {
                wordColor = Color.Black;
            }
            int wordLength = word.Length;
            if(wordColor != rtbSource.SelectionColor)
            {
                rtbSource.Select(charPosition - wordLength, wordLength);
                rtbSource.SelectionColor = wordColor;
                if (boldFont)
                    rtbSource.SelectionFont = BoldStyle;
                else
                    rtbSource.SelectionFont = NormalStyle;
                rtbSource.SelectionLength = 0;
            }
        }

        private void ColorizeLine(int lineIndex, bool OneLine)
        {
            if (OneLine)
            {
                rtbSource.SuspendLayout();
            }

            WordColorizingEnabled = false;
            int firstChar = rtbSource.GetFirstCharIndexFromLine(lineIndex);

            int oldPos = rtbSource.SelectionStart;
            string lineText = rtbSource.Lines[lineIndex] + "\n";
            StringBuilder lastChars = new StringBuilder();
            for (int i = 0; i < lineText.Length; i++)
            {
                char c = lineText[i];
                int realCharIndex = firstChar + i;

                if (Array.IndexOf(Terminals, c) > -1)
                {
                    if (lastChars.Length > 0)
                    {
                        string lastWord = lastChars.ToString();
                        ColorizeWord(lastWord, realCharIndex);
                        lastChars.Remove(0, lastChars.Length);
                    }
                    if (c == '(' || c == ')' || c == '\'')
                    {
                        ColorizeWord(c.ToString(), realCharIndex + 1);
                    }
                }
                else
                    lastChars.Append(c);
            }
            rtbSource.SelectionColor = Color.Black;
            rtbSource.SelectionStart = oldPos;
            rtbSource.SelectionLength = 0;
            WordColorizingEnabled = true;

            if (OneLine)
            {
                rtbSource.ResumeLayout();
            }
        }

        private void ColorizeTextBox()
        {
            rtbSource.SuspendLayout();
            for (int i = 0; i < rtbSource.Lines.Length; i++)
            {
                ColorizeLine(i, false);
            }
            rtbSource.ResumeLayout();
        }

        private void rtbSource_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
                rtbSource.SelectedText = new string(' ', TAB_SIZE);
        }

        private void rtbSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            char newChar = e.KeyChar;
            if (newChar == '\r')
            {
                rtbSource.SelectionColor = Color.Black;
                int prevLineIndex = rtbSource.GetLineFromCharIndex(rtbSource.SelectionStart) - 1;
                ColorizeLine(prevLineIndex, true);
                string line = rtbSource.Lines[prevLineIndex];
                int indent;
                for (indent = 0; indent < line.Length && line[indent] == ' '; indent++) ;
                rtbSource.SelectedText = new string(' ', indent);
            }
        }

        private void rtbSource_TextChanged(object sender, EventArgs e)
        {
            if (WordColorizingEnabled)
            {
                int charPosition = rtbSource.SelectionStart;
                int firstCharPosition = rtbSource.GetFirstCharIndexOfCurrentLine();
                int relativePosition = charPosition - firstCharPosition;
                int lineIndex = rtbSource.GetLineFromCharIndex(charPosition);

                if (rtbSource.Lines.Length > 0 && relativePosition > 0)
                {
                    string currentLine = rtbSource.Lines[lineIndex];
                    int findFromCharPosition = relativePosition;
                    int leftTermIndex = currentLine.LastIndexOfAny(Terminals, findFromCharPosition - 1);
                    if (leftTermIndex == -1)
                        leftTermIndex = 0;
                    int rightTermIndex = currentLine.IndexOfAny(Terminals, findFromCharPosition);
                    if (rightTermIndex == -1)
                        rightTermIndex = currentLine.Length;
                    int wordLength = rightTermIndex - leftTermIndex;
                    string word = currentLine.Substring(leftTermIndex, wordLength);
                    int oldPos = rtbSource.SelectionStart;
                    if (word.Length > 1 && Array.IndexOf(Terminals, word[0]) > -1)
                    {
                        word = word.Substring(1);
                        leftTermIndex++;
                    }
                    rtbSource.SuspendLayout();
                    ColorizeWord(word, firstCharPosition + rightTermIndex);
                    rtbSource.ResumeLayout();
                    rtbSource.SelectionStart = oldPos;
                }
            }
        }
    }
}
