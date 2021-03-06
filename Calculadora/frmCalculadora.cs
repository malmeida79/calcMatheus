﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MotorDeCalculo;

namespace Calculadora
{
    public partial class frmCalculadora : Form
    {
        #region Declarations

        private List<decimal> numbers = new List<decimal>();
        private List<string> sinals = new List<string>();
        private string ultimoSinal = "";
        private decimal ultimoNumero = 0;

        public frmCalculadora()
        {
            InitializeComponent();
        }

        #endregion

        #region Botoes 0 - 9

        private void btn1_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            SetDisplayNumber(sender);
        }

        #endregion

        #region Botoes especiais

        private void btnComma_Click(object sender, EventArgs e)
        {
            if (PodeVirgula())
            {
                SetDisplayNumber(sender);
            }
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            Operator opr = new Operator();
            txtDisplay.Text = opr.Square(Convert.ToDouble(txtDisplay.Text)).ToString();
        }

        private void btnCE_Click(object sender, EventArgs e)
        {
            LimpaDados();
        }

        #endregion

        #region Operações

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        private void btnLess_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            AddOperator(sender);
        }

        #endregion

        #region Finalizacao

        private void btnEqual_Click(object sender, EventArgs e)
        {
            // quando clicado igual, o ultimo numero inserido ainda nao esta
            // na lista de calculos
            numbers.Add(decimal.Parse(txtDisplay.Text));

            Operator opr = new Operator();

            // apresenta o total
            txtDisplay.Text = opr.ProcessaFila(numbers, sinals).ToString();

            // recupera o erro
            var erro = opr.GetErro();

            if (ultimoNumero == 0)
            {
                // guardando ultimo numero e sinal inseridos
                ultimoSinal = sinals[0];
                ultimoNumero = numbers[1];
            }

            // limpa dados (mantem a tela e vars de controle)
            LimpaDados(true, false);

            // reinicia as listas com o ultimo numero e sinal caso o usuario queira 
            // apertar o igual diversas vezes
            sinals.Add(ultimoSinal);
            numbers.Add(ultimoNumero);
        }

        /// <summary>
        /// Adicona o numero e operador para conta
        /// </summary>
        /// <param name="sender">evendo disparador do qual queremos o texto.</param>
        private void AddOperator(object sender)
        {
            string operacao = (sender as Button).Text;

            // enquanto esse numero for zero entendemos que[
            // nao foi clicado nenhuma vez o igual
            if (ultimoNumero <= 0)
            {
                sinals.Add(operacao);
                numbers.Add(decimal.Parse(txtDisplay.Text));
                txtDisplay.Clear();
            }
            else
            {
                // entendemos aqui que o igual ja tinha sido
                // clicado antes entao, precisamos limpar os
                // dados e o ultimo numero passa a ser entao
                // do display.
                LimpaDados(true);
                sinals.Add(operacao);
                numbers.Add(decimal.Parse(txtDisplay.Text));
                txtDisplay.Clear();
            }
        }

        #endregion

        #region Metodos Internos



        /// <summary>
        /// leva um numero ate o display
        /// </summary>
        /// <param name="sender">objeto disparador do qual queremos o texto</param>
        private void SetDisplayNumber(object sender)
        {
            string number = (sender as Button).Text;

            if (number == "+/-")
            {
                if (txtDisplay.Text.Length > 0)
                {
                    var tratamento = Convert.ToDecimal(txtDisplay.Text) * -1;
                    txtDisplay.Text = tratamento.ToString();
                }
            }
            else
            {
                txtDisplay.Text += number;
            }
        }

        /// <summary>
        /// leva um numero ate o display
        /// </summary>
        /// <param name="sender">objeto disparador do qual queremos o texto</param>
        private void SetDisplayNumber(string number)
        {
            txtDisplay.Text += number;
        }

        /// <summary>
        /// Limpa os dados da memoria e o display
        /// </summary>
        /// <param name="materDisplay">deseja manter display com dados?</param>
        /// <param name="limpaControle">deseja limpar valores de controle?</param>
        private void LimpaDados(bool materDisplay = false, bool limpaControle = true)
        {
            if (!materDisplay)
            {
                txtDisplay.Text = "";
            }

            if (limpaControle)
            {
                ultimoSinal = "";
                ultimoNumero = 0;
            }

            sinals.Clear();
            numbers.Clear();
        }

        private bool PodeVirgula()
        {
            return (!txtDisplay.Text.Contains(",") && txtDisplay.Text.Length > 0);
        }

        #endregion

        #region Teclas

        private void frmCalculadora_KeyPress(object sender, KeyPressEventArgs e)
        {
            List<char> numeros = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            char tecla = e.KeyChar;

            if (numeros.Contains(tecla))
            {
                SetDisplayNumber(tecla.ToString());
            }
            else if (tecla == '\b')
            {
                btnBack.Focus();
                btnBack_Click(sender, e);
            }
            else if (tecla == '\u001b')
            {
                btnCE.Focus();
                btnCE_Click(sender, e);
            }
            else if (tecla == ',')
            {
                if (PodeVirgula())
                {
                    SetDisplayNumber(",");
                }
            }
        }

        #endregion   
    }
}
