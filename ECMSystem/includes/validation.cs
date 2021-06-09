using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ECMSystem.includes
{
    class validation
    {
        //by sandakelum priyamantha
        int number_of_fill_textBox =0;
       
        public bool is_select_time(TimePicker timePicker)
        {
            if (timePicker.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int check_timePickers(TimePicker[] timePickers,TextBlock[] textBlocks)
        {
            int tot = 0;
            for(int i=0;i < timePickers.Length; i++)
            {
                if (is_select_time(timePickers[i]))
                {
                    tot++;
                    textBlocks[i].Text = "*";
                }
                else
                {
                    textBlocks[i].Text = "Please select time";
                }
            }
            return tot;
        }

        public bool is_empty(TextBox txtbox,TextBlock textBlock)
        {
            if(txtbox.Text == "")
            {
                textBlock.Text = "This field is required";
                return false;
            }
            else
            {
                number_of_fill_textBox = number_of_fill_textBox + 1;
                return true;
            }
            
        }

        public int check_comboBoxes(ComboBox[] comboBoxes,TextBlock[] textBlocks)
        {
            int tot = 0;
            for(int i = 0; i < comboBoxes.Length; i++)
            {
                if (is_selectItem(comboBoxes[i]))
                {
                    
                    textBlocks[i].Text = "*";
                    tot++;

                }
                else
                {
                    textBlocks[i].Text = "Please selete item";
                }
            }
            return tot;
        }



        public string check_len(string str,int len)
        {
            if(str.Length < len)
            {
                return "At least " + len.ToString() + " char require";
            }
            else
            {
                return "";
            }
        }

        public int number_of_fill(TextBox[] textBoxes,TextBlock[] textBlocks)
        {
            
            for (int i = 0; i < textBoxes.Length; i++)
            {
                
                is_empty(textBoxes[i],textBlocks[i]);
            }

            return number_of_fill_textBox;
            
        }

        public bool is_setDate(DatePicker date)
        {
            if(date.SelectedDate == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool is_selectItem(ComboBox combo)
        {
            if(combo.SelectedIndex == -1)
            {
                return false;

            }
            else
            {
                return true;
            }
        }

        public bool is_number_only(string str)
        {

            if (str.All(char.IsDigit))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
