using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
Author's name: Alexandrescu Andrei-Robert
Group: 911
School Year: 2019-2020

METHODS MAP:
-winforms events - 1
-checks - 2
-conversions - 3
-main features - 4
-misc - 5
*/

namespace CLOptionalHomework_csharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*1
             Description: Sets the index of the combobox with the method to 0
            */
            cbCon3.SelectedIndex = 0;
        }

        private void CbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*1
             Description: changes different UI features when the selected value from the operations combobox is changed
            */
            switch (cbOperation.SelectedIndex)
            {
                case 0:
                    //addition
                    panelOperation.Visible = true;
                    lbOp1.Text = "Addend1:";
                    lbOp2.Text = "Addend2:";
                    lbOp4.Text = "Sum";
                    lbOp5.Visible = false;
                    tbOp2.Width = tbOp1.Width;
                    tbOp2.MaxLength = 64;
                    tbOp1.Text = "";
                    tbOp2.Text = "";
                    cbOp1.SelectedIndex = -1;
                    tbResult1.Text = "";
                    tbRemainder.Visible = false;
                    break;
                case 1:
                    //subtraction
                    panelOperation.Visible = true;
                    lbOp1.Text = "Minuend:";
                    lbOp2.Text = "Subtrahend:";
                    lbOp4.Text = "Difference";
                    lbOp5.Visible = false;
                    tbOp2.Width = tbOp1.Width;
                    tbOp2.MaxLength = 64;
                    tbOp1.Text = "";
                    tbOp2.Text = "";
                    cbOp1.SelectedIndex = -1;
                    tbResult1.Text = "";
                    tbRemainder.Visible = false;
                    break;
                case 2:
                    //multiplication
                    panelOperation.Visible = true;
                    lbOp1.Text = "Integer:";
                    lbOp2.Text = "Digit:";
                    lbOp4.Text = "Result";
                    lbOp5.Visible = false;
                    tbOp2.Width = 47;
                    tbOp2.MaxLength = 1;
                    tbOp1.Text = "";
                    tbOp2.Text = "";
                    cbOp1.SelectedIndex = -1;
                    tbResult1.Text = "";
                    tbRemainder.Visible = false;
                    break;
                case 3:
                    //division
                    panelOperation.Visible = true;
                    lbOp1.Text = "Divident:";
                    lbOp2.Text = "Digit:";
                    lbOp4.Text = "Quotient";
                    lbOp5.Visible = true;
                    tbOp2.Width = 47;
                    tbOp2.MaxLength = 1;
                    tbOp1.Text = "";
                    tbOp2.Text = "";
                    cbOp1.SelectedIndex = -1;
                    tbResult1.Text = "";
                    tbRemainder.Visible = true;
                    break;
                default:
                    break;
            }
        }

        private void BtnGo1_Click(object sender, EventArgs e)
        {
            /*1
             Description: the click event for the button which performs different operations
            */
            //called on BtnGo1 click
            //first the input fields are validated
            if (string.IsNullOrWhiteSpace(tbOp1.Text) || string.IsNullOrWhiteSpace(tbOp2.Text))
            {
                MessageBox.Show("All spaces must be filled");
            } else if (cbOp1.SelectedIndex < 0)
            {
                MessageBox.Show("Select a base");
            }else if (cbOp1.SelectedIndex == 3 && tbOp2.Text == "0")
            {
                MessageBox.Show("Can not divide by 0");
            }
            else
            {
                //then we check of the inputed values are representations in the chosen base
                if (checkBaseCorrectness(tbOp1.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString()) && checkBaseCorrectness(tbOp2.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString()))
                {
                    string v1 = tbOp1.Text, v2 = tbOp2.Text;
                    remove_zeros(ref v1);
                    remove_zeros(ref v2);
                    tbOp1.Text = v1;
                    tbOp2.Text = v2;
                    //then we perform the operations
                    switch (cbOperation.SelectedIndex)
                    {
                        case 0:
                            //addition
                            tbResult1.Text = add(tbOp1.Text, tbOp2.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString());
                            break;
                        case 1:
                            //subtraction
                            tbResult1.Text = subtract(tbOp1.Text, tbOp2.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString());
                            break;
                        case 2:
                            //multiplication
                            tbResult1.Text = multiply(tbOp1.Text, tbOp2.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString());
                            break;
                        case 3:
                            //division
                            tbResult1.Text = divide(tbOp1.Text, tbOp2.Text, cbOp1.Items[cbOp1.SelectedIndex].ToString());
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("The selected integers are not representations in base " + cbOp1.Items[cbOp1.SelectedIndex]);
                }
            }
        }

        private void BtnGo2_Click(object sender, EventArgs e)
        {
            /*1
             Description: the click event for the button which converts the given numbers
            */
            //clearing tbLog
            tbLog.Text = "";
            tbLog.SelectionStart = 0;
            tbLog.ScrollToCaret();
            //checking phase
            string msg = checkConversionCorrectness();
            if (msg == null)
            {
                string result = "";
                int b1Int = Convert.ToInt32(cbCon1.SelectedItem.ToString());
                int b2Int = Convert.ToInt32(cbCon2.SelectedItem.ToString());
                //what method to use?
                int method = cbCon3.SelectedIndex;
                if (method == 0)
                {
                    method = choose_conversion_method(tbCon1.Text, b1Int, b2Int);
                }
                switch (method)
                {
                    case 1:
                        result = rapidConversionMethod(tbCon1.Text, b1Int, b2Int);
                        if (result == "Error")
                        {
                            result = "Impossible";
                        }
                        break;
                    case 2:
                        result = successiveDivisionsMethod(tbCon1.Text, b1Int, b2Int);
                        if (result == "Error")
                        {
                            result = "Impossible";
                        }
                        break;
                    case 3:
                        result = substitutionConversionMethod(tbCon1.Text, b1Int, b2Int);
                        break;
                    case 4:
                        int b3Int = Convert.ToInt32(cbCon4.SelectedItem.ToString());
                        result = intermediateBaseMethod(tbCon1.Text, b1Int, b2Int, b3Int);
                        break;
                    default:
                        break;
                }
                tbResult2.Text = result;
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        private void CbCon3_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*1
            Description: changes different UI features when the selected value from the operations combobox is changed
            */
            if (cbCon3.SelectedIndex == 4)
            {
                lbCon7.Visible = true;
                cbCon4.Visible = true;
            }
            else if (cbCon4.Visible == true)
            {
                lbCon7.Visible = false;
                cbCon4.Visible = false;
                cbCon4.SelectedIndex = -1;
            }
        }

        private bool checkBaseCorrectness(string rep, string baseOp)
        {
            /*2
             * Description: checks wether the characters of a given string are representations in a given base
             * Input: rep - the string representation
                      baseOp - the base
             * Output: True if all characters of the given string are representations in the given base
            */
            int index = 0;
            if (baseOp == "16")
            {
                while (index < rep.Length)
                {
                    if (!(((int)rep[index] >= 48 && (int)rep[index] <= 57) || ((int)rep[index] >= 65 && (int)rep[index] <= 70) || ((int)rep[index] >= 97 && (int)rep[index] <= 102)))
                    {
                        return false;
                    }
                    index++;
                }
            } else if (baseOp == "10")
            {
                while (index < rep.Length)
                {
                    if (rep[index] > (int)'9')
                    {
                        return false;
                    }
                    index++;
                }
            }
            else
            {
                char baseOpChar = Convert.ToChar(baseOp);
                while (index < rep.Length)
                {
                    if (rep[index] >= baseOpChar)
                    {
                        return false;
                    }
                    index++;
                }
            }
            return true;
        }

        private string checkConversionCorrectness()
        {
            /*2
             * Description: checks wether the given data for conversions is in a valid format
             * Input: 
             * Output: -an error message if case
                       -null otherwise
            */
            //checking if fields are valid
            if (string.IsNullOrWhiteSpace(tbCon1.Text))
            {
                return "All fields must be filled";
            }
            else if (cbCon1.SelectedIndex < 0 || cbCon2.SelectedIndex < 0 || (cbCon4.SelectedIndex < 0 && cbCon4.Visible == true))
            {
                return "All base fields must be filled";
            }
            else if (!checkBaseCorrectness(tbCon1.Text, cbCon1.SelectedItem.ToString()))
            {
                return tbCon1.Text + " is not a representation in base " + cbCon1.SelectedItem.ToString();
            }
            return null;
        }

        private int convertToDecimal(char value)
        {
            /*3
             * Description: converts a character to decimal
             * Input: value - the character to be converted
             * Output: the decimal representation of the inputed character
            */
            if ((int)value >= 48 && (int)value <= 57)
            {
                return (int)value - (int)'0';
            }
            else if ((int)value >= 65 && (int)value <= 71)
            {
                return (int)value - (int)'A' + 10;
            }
            else if ((int)value >= 97 && (int)value <= 102)
            {
                return (int)value - (int)'a' + 10;
            }
            return 0;
        }

        private int convertStringToDecimal(string value, int baseOp)
        {
            /*3
             * Description: converts a string from a specified base to decimal
             * Input: value - the string to be converted
                      baseOp - the base to convert from
             * Output: the decimal representation of the inputed string
            */
            int result = 0, d, p = 0;
            for (int i = value.Length - 1; i >= 0; i--)
            {
                d = convertToDecimal(value[i]);

                result += d * Convert.ToInt32(Math.Pow(Convert.ToDouble(baseOp), p));
                p++;
            }
            return result;
        }

        private string convertToBinary(char value, int baseOp)
        {
            /*3
             * Description: converts a digit to binary
             * Input: value - the number to be converted
                      baseOp - the base to convert from
             * Output: the converted string
            */
            string res = "";
            int number = 0;
            if (baseOp == 16 && (int)value >= (int)'A')
            {
                //base 16
                if (value >= 'A' && value <= 'F')
                {
                    number = (int)value - (int)'A' + 10;
                }
                else
                {
                    number = (int)value - (int)'a' + 10;
                }
            }
            else
            {
                number = (int)value - (int)'0';
            }

            while (number > 0)
            {
                if (number % 2 == 0)
                {
                    res += "0";
                }
                else
                {
                    res += "1";
                }
                number /= 2;
            }

            if (baseOp == 4 && res.Length < 2)
            {
                add_zeros(ref res, 2 - res.Length);
            }
            else if (baseOp == 8 && res.Length < 3)
            {
                add_zeros(ref res, 3 - res.Length);
            }
            else if (baseOp == 16 && res.Length < 4)
            {
                add_zeros(ref res, 4 - res.Length);
            }

            reverse_string(ref res);

            return res;
        }

        private string convertFromBinary(string value, int baseOp)
        {
            /*3
             * Description: converts to specified base from binary
             * Input: value - the binary representation to be converted
                      baseOp - the base to convert from
             * Output: the converted string
            */
            int p = 0;
            double res = 0;

            int index = value.Length - 1;
            while (index >= 0)
            {
                if (value[index] != '0')
                {
                    res += Math.Pow(2, p);
                }
                p++;
                index--;
            }

            string resStr = "0";
            if (baseOp == 16 && Convert.ToInt32(res) >= 10)
            {
                resStr = (Convert.ToChar((int)'A' + Convert.ToInt32(res) - 10)).ToString();
            }
            else
            {
                resStr = Convert.ToInt32(res).ToString();
            }
            return resStr;
        }

        private string convertDigitToBase(char digit, int b1, int b2)
        {
            /*3
             * Description: converts a digit to a specified base
             * Input: digit - the digit to be converted
                      b1 - the base to convert from
                      b1 - the base to convert to
             * Output: the converted digit
            */
            string res = "";
            if (b1 < b2)
            {
                res = digit.ToString();
            }
            else
            {
                res = successiveDivisionsMethod(digit.ToString(), b1, b2);
            }

            return res;
        }

        private string convertAllDigits(string value, int b1, int b2)
        {
            /*3
             * Description: converts all digits of a number to the specified base
             * Input: value - the number to be converted
                      b1 - the base to convert from
                      b1 - the base to convert to
             * Output: the converted string
            */
            //pretty useless, if b1<b2, all digits in b1 are the same in b2
            string res = "";
            for (int i = 0; i < value.Length; i++)
            {
                res += convertDigitToBase(value[i], b1, b2);
            }
            return res;
        }

        private string add(string v1, string v2, string baseOp)
        {
            /*4
             * Description: performs the addition of two numbers in a given base
             * Input: v1 - the first addend
                      v2 - the second addend
                      baseOp - the base in which the addition takes place
             * Output: the result of the addition
            */

            //v stands for value
            //v1.length has to be >= v2.length
            if (v1.Length < v2.Length)
            {
                string plc = v1;
                v1 = v2;
                v2 = plc;
            }

            //the strings are reversed
            reverse_string(ref v1);
            reverse_string(ref v2);

            //fill with zeros
            add_zeros(ref v2, v1.Length - v2.Length);

            //convert the base into decimal
            int baseOpInt = 0;
            if (baseOp == "16")
            {
                baseOpInt = convertToDecimal('F') + 1;
            }
            else if (baseOp == "10")
            {
                baseOpInt = convertToDecimal('A');
            }
            else
            {
                baseOpInt = convertToDecimal(Convert.ToChar(baseOp));
            }

            //operations
            int sum, rem = 0, d1, d2, i = 0;
            string result = "";
            char dig;
            while (i < v1.Length)
            {
                //convert the digits to decimal
                d1 = convertToDecimal(v1[i]);
                d2 = convertToDecimal(v2[i]);
                sum = d1 + d2 + rem;
                if (sum >= baseOpInt)
                {
                    //passing remainder
                    rem = 1;
                    if (baseOpInt == 16)
                    {
                        int r = sum % baseOpInt;
                        if (r >= 10)
                        {
                            r -= 10;
                            dig = (char)(r + (int)'A');
                        }
                        else
                        {
                            dig = (char)(r + (int)'0');
                        }
                    }
                    else
                    {
                        dig = (char)(sum % baseOpInt + (int)'0');
                    }
                    result += dig;
                }
                else
                {
                    //no remainder is passed
                    rem = 0;
                    if (baseOpInt == 16)
                    {
                        if (sum >= 10)
                        {
                            dig = (char)(sum + (int)'A' - 10);
                        }
                        else
                        {
                            dig = (char)(sum + (int)'0');
                        }
                        result += dig.ToString();
                    }
                    else
                    {
                        result += sum.ToString();
                    }
                }
                i++;
            }
            //adding last remained if case
            if (rem > 0)
            {
                result += rem.ToString();
            }

            reverse_string(ref result);
            remove_zeros(ref result);

            return result;
        }

        private string subtract(string v1, string v2, string baseOp)
        {
            /*4
             * Description: performs the subtraction of two numbers in a given base
             * Input: v1 - the minuend
                      v2 - the subtrahend
                      baseOp - the base in which the subtraction takes place
             * Output: the result of the subtraction
            */
            string result = "";
            //checking lengths
            if (v1.Length >= v2.Length)
            {
                //the strings are reversed
                reverse_string(ref v1);
                reverse_string(ref v2);

                //fill with zeros
                add_zeros(ref v2, v1.Length - v2.Length);

                //convert the base into decimal
                int baseOpInt = 0;
                if (baseOp == "16")
                {
                    baseOpInt = convertToDecimal('F') + 1;
                }
                else if (baseOp == "10")
                {
                    baseOpInt = convertToDecimal('A');
                }
                else
                {
                    baseOpInt = convertToDecimal(Convert.ToChar(baseOp));
                }

                //operations
                int dif, rem = 0, d1, d2, i = 0;
                char dig;
                while (i < v1.Length)
                {
                    //convert the digits to decimal
                    d1 = convertToDecimal(v1[i]);
                    d2 = convertToDecimal(v2[i]);
                    dif = rem + d1 - d2;
                    if (dif < 0)
                    {
                        //add the base
                        dif += baseOpInt;
                        rem = -1;
                    }
                    else
                    {
                        rem = 0;
                    }

                    if (baseOpInt == 16 && dif >= 10)
                    {
                        dig = Convert.ToChar(dif + (int)'A' - 10);
                    }
                    else
                    {
                        dig = Convert.ToChar(dif + (int)'0');
                    }

                    result += dig.ToString();

                    i++;
                }
            }
            else
            {
                MessageBox.Show("The minuend must be greater than the subtrahend");
                return "Error";
            }
            reverse_string(ref result);
            remove_zeros(ref result);

            return result;
        }

        private string multiply(string v1, string v2, string baseOp)
        {
            /*4
             * Description: multiplies a number by a digit in a given base
             * Input: v1 - the number to be multiplied
                      v2 - the digit to multiply the number with
                      baseOp - the base in which the multiplication takes place
             * Output: the result of the multiplication
            */
            string result = "";

            //the strings are reversed
            reverse_string(ref v1);

            //convert the base into decimal
            int baseOpInt = 0;
            if (baseOp == "16")
            {
                baseOpInt = convertToDecimal('F') + 1;
            } else if (baseOp == "10")
            {
                baseOpInt = convertToDecimal('A');
            }
            else
            {
                baseOpInt = convertToDecimal(Convert.ToChar(baseOp));
            }

            //operations
            int prod, rem = 0, d1, d2, i = 0, qt = 0;
            char dig;

            d2 = convertToDecimal(v2[0]);

            while (i < v1.Length)
            {
                //convert the digits to decimal
                d1 = convertToDecimal(v1[i]);
                prod = qt + d1 * d2;
                qt = prod / baseOpInt;
                rem = prod % baseOpInt;

                if (baseOpInt == 16 && rem >= 10)
                {
                    dig = Convert.ToChar(rem + (int)'A' - 10);
                }
                else
                {
                    dig = Convert.ToChar(rem + (int)'0');
                }

                result += dig;

                i++;
            }

            if (qt > 0)
            {
                if (baseOpInt == 16 && qt >= 10)
                {
                    dig = Convert.ToChar(qt + (int)'A' - 10);
                }
                else
                {
                    dig = Convert.ToChar(qt + (int)'0');
                }
                result += dig;
            }

            reverse_string(ref result);

            return result;
        }

        private string divide(string v1, string v2, string baseOp)
        {
            /*4
             * Description: divides a number to a digit in a given base
             * Input: v1 - the divident
                      v2 - the digit to divide the divident with
                      baseOp - the base in which the division takes place
             * Output: the result of the division
            */

            string result = "";

            //convert the base into decimal
            int baseOpInt = 0;
            if (baseOp == "16")
            {
                baseOpInt = convertToDecimal('F') + 1;
            }
            else if (baseOp == "10")
            {
                baseOpInt = convertToDecimal('A');
            }
            else
            {
                baseOpInt = convertToDecimal(Convert.ToChar(baseOp));
            }

            //operations
            int rem = 0, d1, d2, i = 0, qt = 0;
            char dig;

            d2 = convertToDecimal(v2[0]);

            while (i < v1.Length)
            {
                //convert the digits to decimal
                if (rem >= 10)
                {
                    char c = Convert.ToChar((int)'A' + rem - 10);
                    d1 = convertStringToDecimal(c.ToString() + v1[i], baseOpInt);
                }
                else
                {
                    d1 = convertStringToDecimal(rem.ToString() + v1[i], baseOpInt);
                }

                qt = d1 / d2;
                rem = d1 % d2;
                if (baseOpInt == 16 && qt >= 10)
                {
                    dig = Convert.ToChar(qt + (int)'A' - 10);
                }
                else
                {
                    dig = Convert.ToChar(qt + (int)'0');
                }

                result += dig;

                i++;
            }

            if (rem >= 10)
            {
                char c = Convert.ToChar((int)'A' + rem - 10);
                tbRemainder.Text = c.ToString();
            }
            else
            {
                tbRemainder.Text = rem.ToString();
            }

            remove_zeros(ref result);
            
            return result;
        }


        private int choose_conversion_method(string s1, int b1, int b2)
        {
            /*4
             * Description: chooses a proper conversion method
             * Input: s1 - the string to be analysed
                      b1 - the base to convert from
                      b2 - the base to convert to
             * Output: a number representing the conversion code:
                        1 for rapid conversions
                        2 for successive divisions
                        3 for substitution method
            */
            if ((b1 == 2 || b1 == 4 || b1 == 8 || b1 == 16)&&(b2 == 2 || b2 == 4 || b2 == 8 || b2 == 16))
            {
                //rapid conversions
                return 1;
                
            }
            else if(b1 >= b2)
            {
                //successive divisions
                return 2;
            }
            else
            {
                //substitution method
                return 3;
            }
        }

        private string rapidConversionMethod(string s1, int b1, int b2)
        {
            /*4
             * Description: converts a given string using the rapid conversion method
             * Input: s1 - the string to be converted
                      b1 - the base to convert from
                      b2 - the base to convert to
             * Output: the representation of the given string in base b2
            */
            string res = "";

            if ((b1 == 2 || b1 == 4 || b1 == 8 || b1 == 16) && (b2 == 2 || b2 == 4 || b2 == 8 || b2 == 16))
            {
                tbLog.AppendText(">Started Rapid conversion"+ "\r\n");//Logs
                tbLog.AppendText("->Converting" + s1 + " from base " + b1.ToString() + " to base " + b2.ToString() + "\r\n");
                //decompose
                if (b1 != 2)
                {
                    int index = 0;
                    while (index < s1.Length)
                    {
                        res += convertToBinary(s1[index], b1);
                        index++;
                    }
                    tbLog.AppendText("*" + b1.ToString() + " -> 2: " + res + "\r\n");//Logs
                }
                else
                {
                    res = s1;
                }

                string final_res = res;

                //recompose
                if (b2 != 2)
                {
                    res = "";

                    int index;
                    int step = 1;
                    switch (b2)
                    {
                        case 4:
                            step = 2;
                            break;
                        case 8:
                            step = 3;
                            break;
                        case 16:
                            step = 4;
                            break;
                    }

                    reverse_string(ref final_res);
                    add_zeros(ref final_res, step - final_res.Length % step);

                    reverse_string(ref final_res);

                    for (index = 0; index < final_res.Length; index += step)
                    {
                        res += convertFromBinary(final_res.Substring(index, step), b2);
                    }
                    
                    final_res = res;

                    remove_zeros(ref final_res);
                    tbLog.AppendText("*2 -> " + b2.ToString() + ": " + final_res + "\r\n");//Logs
                }
                else
                {
                    remove_zeros(ref final_res);
                }

                tbLog.AppendText("*Final result:" + final_res + "\r\n");//Logs
                return final_res;
            }
            else
            {
                return "Error";
            }
        }

        private string successiveDivisionsMethod(string s1, int b1, int b2)
        {
            /*4
             * Description: converts a given string using the successive divisions method
             * Input: s1 - the string to be converted
                      b1 - the base to convert from
                      b2 - the base to convert to
             * Output: the representation of the given string in base b2
            */
            //b1>=b2
            if (b1 >= b2)
            {
                tbLog.AppendText(">Started Successive Divisions" + "\r\n");//Logs
                tbLog.AppendText("->Converting " + s1 + " from base " + b1.ToString() + " to " + b2.ToString() + "\r\n");
                string res = "";
                int value = convertStringToDecimal(s1, b1);
                tbLog.AppendText("-" + s1 + " in decimal is " + value.ToString() + "\r\n");//Logs
                while (value > 0)
                {
                    res += (value % b2).ToString();
                    tbLog.AppendText("-Dividing " + value.ToString() + " by " + b2.ToString() + " ; Result: " + res + "\r\n");//Logs
                    value /= b2;
                }

                reverse_string(ref res);
                tbLog.AppendText("->Reversing result = " + res + "\r\n");//Logs
                
                tbLog.AppendText("*Final result = " + res + "\r\n");//Logs
                return res;
            }
            else
            {
                return "Error";
            }
        }

        private string substitutionConversionMethod(string s1, int b1, int b2)
        {
            /*4
             * Description: converts a given string using the substitution method
             * Input: s1 - the string to be converted
                      b1 - the base to convert from
                      b2 - the base to convert to
             * Output: the representation of the given string in base b2
            */

            //b1<b2
            tbLog.AppendText(">Started Substitution Method" + "\r\n");//Logs
            tbLog.AppendText("->Converting " + s1 + " from base " + b1.ToString() + " to " + b2.ToString() + "\r\n");
            //1-converting all digits from base b1 to base b2
            s1 = convertAllDigits(s1,b1,b2);
            //2-converting base 1 into base 2 - necessary only if b1 == 10 and b2 == 16 or viceversa
            string b1Str = "";
            if (b1 == 10 && b2 == 16)
            {
                b1Str = "A";
            }else if(b1 == 16 && b2 == 10)
            {
                b1Str = "G"; // passing "16 in decimal"
            }

            string res = "", new_res = "";
            int p=0;
            string dStr = "1";
            for(int i=s1.Length-1; i >= 0; i--)
            {
                dStr = "1";
                for(int j = 1; j <= p; j++)
                {
                    if (b1Str != "")
                    {
                        dStr = multiply(dStr, b1Str, b2.ToString());
                    }
                    else
                    {
                        dStr = multiply(dStr, b1.ToString(), b2.ToString());
                    }
                }
                if(res == "")
                {
                    res = multiply(dStr, s1[i].ToString(), b2.ToString());
                }
                else
                {
                    new_res = multiply(dStr, s1[i].ToString(), b2.ToString());
                    res = add(res, new_res, b2.ToString());
                }
                p++;
            }

            tbLog.AppendText("*Final result = " + res + "\r\n");//Logs
            return res;
        }

        private string intermediateBaseMethod(string s1, int b1, int b2, int b3)
        {
            /*4
             * Description: converts a given string using the intermediate base method
             * Input: s1 - the string to be converted
                      b1 - the base to convert from
                      b2 - the base to convert to
                      b3 - the intermediate base
             * Output: the representation of the given string in base b2
            */
            string result = "";
            int method = 0;
            if(b1 != b3)
            {
                method = choose_conversion_method(tbCon1.Text, b1, b3);
                switch (method)
                {
                    case 1:
                        result = rapidConversionMethod(tbCon1.Text, b1, b3);
                        break;
                    case 2:
                        result = successiveDivisionsMethod(tbCon1.Text, b1, b3);
                        break;
                    case 3:
                        result = substitutionConversionMethod(tbCon1.Text, b1, b3);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                result = tbCon1.Text;
            }

            if(b3 != b2)
            {
                method = choose_conversion_method(result, b3, b2);
                switch (method)
                {
                    case 1:
                        result = rapidConversionMethod(result, b3, b2);
                        break;
                    case 2:
                        result = successiveDivisionsMethod(result, b3, b2);
                        break;
                    case 3:
                        result = substitutionConversionMethod(result, b3, b2);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private void add_zeros(ref string v2, int zeros)
        {
            /*5
             * Description: concatenates zeros in front of a string
             * Input: v2 - the main string (value)
                      zeros - the number of zeros to be concatenated
             * Output: 
            */
            while (zeros > 0)
            {
                v2 += "0";
                zeros--;
            }
        }

        private void reverse_string(ref string str)
        {
            /*5
             * Description: reverses the elements of a string
             * Input: str - the string to be reversed
             * Output: 
            */
            string res = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                res += str[i];
            }
            str = res;
        }

        private void remove_zeros(ref string st)
        {
            /*5
             * Description: removes unnecessary zeros in front of the binary input
             * Input: st - the binary number
             * Output: 
            */
            if (st != "0")
            {
                int index = 0;
                while (st[index] == '0')
                {
                    index++;
                }
                if (index > 0)
                {
                    st = st.Substring(index);
                }
            }
        }
    }
}
