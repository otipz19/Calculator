using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Calculator
{
    internal class CalcForm : Form
    {
        private CalcModel model;

        public CalcForm(CalcModel model)
        {
            this.model = model;

            MinimumSize = new Size(200, 400);
            Text = "Simple calculator";

            var mainTable = new TableLayoutPanel() { Dock = DockStyle.Fill };
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            mainTable.Controls.Add(MakeLabelsTable(), 0, 0);
            mainTable.Controls.Add(MakeButtonsTable(), 0, 1);

            Controls.Add(mainTable);
        }

        private Control MakeLabelsTable()
        {
            var labelTable = new TableLayoutPanel() { Dock = DockStyle.Fill, BackColor = Color.Gray };
            for (int i = 0; i < 2; i++)
                labelTable.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            labelTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            Label expression = MakeLabel("", 13);
            model.ExpressionChanged += (str) => expression.Text = str;
            labelTable.Controls.Add(expression, 0, 0);
            Label curOperand = MakeLabel("0", 15);
            model.CurOperandChanged += (str) => curOperand.Text = str;
            labelTable.Controls.Add(curOperand, 0, 1);

            return labelTable;
        }

        private static Label MakeLabel(string text, int fontSize)
        {
            return new Label()
            {
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Right,
                Text = text,
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font(DefaultFont.Name, fontSize)
            };
        }

        private Control MakeButtonsTable()
        {
            var buttonsTable = new TableLayoutPanel() { Dock = DockStyle.Fill };

            for (int i = 0; i < 5; i++)
            {
                buttonsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
                buttonsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
            }

            buttonsTable.Controls.Add(MakeActionButton("<=", (s, a) => model.ClearOneSymbol()), 0, 0);
            buttonsTable.Controls.Add(MakeActionButton("CE", (s, a) => model.ClearCurOperand()), 1, 0);
            buttonsTable.Controls.Add(MakeActionButton("C", (s, a) => model.ClearAll()), 2, 0);
            buttonsTable.Controls.Add(MakeSymbolButton("7"), 0, 1);
            buttonsTable.Controls.Add(MakeSymbolButton("8"), 1, 1);
            buttonsTable.Controls.Add(MakeSymbolButton("9"), 2, 1);
            buttonsTable.Controls.Add(MakeOperatorButton("/"), 3, 1);
            buttonsTable.Controls.Add(MakeSymbolButton("4"), 0, 2);
            buttonsTable.Controls.Add(MakeSymbolButton("5"), 1, 2);
            buttonsTable.Controls.Add(MakeSymbolButton("6"), 2, 2);
            buttonsTable.Controls.Add(MakeOperatorButton("*"), 3, 2);
            buttonsTable.Controls.Add(MakeSymbolButton("1"), 0, 3);
            buttonsTable.Controls.Add(MakeSymbolButton("2"), 1, 3);
            buttonsTable.Controls.Add(MakeSymbolButton("3"), 2, 3);
            buttonsTable.Controls.Add(MakeOperatorButton("-"), 3, 3);
            buttonsTable.Controls.Add(MakeSymbolButton("0"), 0, 4);
            buttonsTable.Controls.Add(MakeSymbolButton(","), 1, 4);
            buttonsTable.Controls.Add(MakeActionButton("=", (s, a) => model.GetResult()), 2, 4);
            buttonsTable.Controls.Add(MakeOperatorButton("+"), 3, 4);

            return buttonsTable;
        }

        private Control MakeButton(string text)
        {
            return new Button() { Dock = DockStyle.Fill, Text = text, Font = new Font(DefaultFont.Name, 12) };
        }

        private Control MakeSymbolButton(string symbol)
        {
            var button = MakeButton(symbol);
            button.Click += (s, a) => model.AddSymbol(symbol);
            return button;
        }

        private Control MakeOperatorButton(string op)
        {
            var button = MakeButton(op);
            button.Click += (s, a) => model.AddBinaryOperator(op);
            return button;
        }

        private Control MakeActionButton(string label, EventHandler action)
        {
            var button = MakeButton(label);
            button.Click += action;
            return button;
        }
    }
}
