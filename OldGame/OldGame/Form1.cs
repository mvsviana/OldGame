using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OldGame
{
    public partial class Painel : Form
    {
        //Sempre inicializa o programa com a vez do "X".
        bool VezX = true;
        //Inicialização do placar
        int placax = 0, placao = 0;

        int Timerleft;

        public Painel()
        {
            InitializeComponent();
        }

        private void Painel_Load(object sender, EventArgs e){
            //Cria um evento ao clickar em cada botão do painel.
            button11.Click += new EventHandler(ButtonClick);
            button12.Click += new EventHandler(ButtonClick);
            button13.Click += new EventHandler(ButtonClick);
            button21.Click += new EventHandler(ButtonClick);
            button22.Click += new EventHandler(ButtonClick);
            button23.Click += new EventHandler(ButtonClick);
            button31.Click += new EventHandler(ButtonClick);
            button32.Click += new EventHandler(ButtonClick);
            button33.Click += new EventHandler(ButtonClick);

            //Sempre começa com o "X".
            buttonO.Enabled = false;

            //Inicialização do placar.
            PlacarX.Text = Convert.ToString(placax);
            PlacarO.Text = Convert.ToString(placao);

            //Sempre começa com o "X".
            buttonO.Enabled = false;

            DesabilitandoTodosBotoes();

            //Faz com que ao clicar no botão não pule para o prox.
            foreach (Control item in this.Controls){

                if (item is Button){ //Verifica se é um botão então ele não permiti pular para o prox.
                    item.TabStop = false;
                }

            }
        }
        //Metodo para marcação de jogadas, "X" e "O".
        private void ButtonClick(object sender, EventArgs e)
        {
            //Acessando as propriedades.
            ((Button)sender).Text = this.VezX ? "X" : "O"; //Se for a vez do "X", ele insere o X senão insere o "O".
            ((Button)sender).Enabled = false; //Assim que inserir ele desabilita o butão.
            VezX = !VezX; //Quando for a vez do "O"

            //Chama metodo para iniciar uma nova contagem.
            Tempo();
            
            //Chama variavel para fazer a marcação de jogada.
            MarcaTextBTN();

            //Chama o metodo de verificação da partida.
            VerificaJogo();

        }
        private void MarcaTextBTN()
        {
            //Verifica e habilita o botão do jogador da vez.
            if (VezX)
            { //Se for a vez do "X".
                buttonO.Enabled = false;//Desabilita o botão "O".
                buttonX.Enabled = true;//Habilita o botão "X".
            }
            else
            { //Senão for a vez do "X".
                buttonO.Enabled = true; //Habilita o botão "O"
                buttonX.Enabled = false; //Desabilita o botão "X"
            }
        }
        //Metodo de verificação da partida.
        private void VerificaJogo()
        {
            //Verificação se à ganhador no jogo.
            if (
                //Verificação da linha 1
                button11.Text != string.Empty && button11.Text == button12.Text && button12.Text == button13.Text ||
                //Verificação da linha 2
                button21.Text != string.Empty && button21.Text == button22.Text && button22.Text == button23.Text ||
                //Verificação da linha 3
                button31.Text != string.Empty && button31.Text == button32.Text && button32.Text == button33.Text ||

                //Verificação da coluna 1
                button11.Text != string.Empty && button11.Text == button21.Text && button21.Text == button31.Text ||
                //Verificação da coluna 2
                button12.Text != string.Empty && button12.Text == button22.Text && button22.Text == button32.Text ||
                //Verificação da coluna 3
                button13.Text != string.Empty && button13.Text == button23.Text && button23.Text == button33.Text ||

                //Verificação da diagonal principal
                button11.Text != string.Empty && button11.Text == button22.Text && button22.Text == button33.Text ||
                //Verificação da diagonal secundaria
                button13.Text != string.Empty && button13.Text == button22.Text && button22.Text == button31.Text
                )
            {
                ContadorTempo.Stop();//Para a contagem do tempo.
                TimerLeft.Text = "TEMPO: ";//Volta ao texto padrão do contador de tempo.

                //Chama metodo de desabilita os botões.   
                DesabilitandoTodosBotoes();

                //Chama o metodo de placar.
                Placarxo();

                Iniciar.Enabled = true;// O botão é habilitado para inicar o jogo novamente
                Reiniciar.Enabled = false; // O botão é desabilitado, sendo habilitado somente quando o jogo for inicializado.

                //Informa quem é o ganhador.
                MessageBox.Show(string.Format("O ganhador da rodada é: {0}", !VezX ? "X" : "O"));

                //Mostra o placar atualizado.
                PlacarX.Text = Convert.ToString(placax);
                PlacarO.Text = Convert.ToString(placao);

            }
            // Caso não tenha ganhador, chama o metodo de verificação de empate.
            else
            {
                //Chamada de metodo.
                VerificaEmpate();
            }

        }

        //Metodo para a desabilitação dos botões.
        private void DesabilitandoTodosBotoes()
        {

            //Verifica se à botões habilitados e desabilita, evitando jogadas após uma vitoria. 
            if (button11.Enabled)
            { // Botão 1 da primeira linha.
                button11.Enabled = false;
            }
            if (button12.Enabled)
            { // Botão 2 da primeira linha.
                button12.Enabled = false;
            }
            if (button13.Enabled)
            { // Botão 3 da primeira linha.
                button13.Enabled = false;
            }
            if (button21.Enabled)
            { // Botão 1 da segunda linha.
                button21.Enabled = false;
            }
            if (button22.Enabled)
            { //Botão 2 da segunda linha.
                button22.Enabled = false;
            }
            if (button23.Enabled)
            { //Botão 3 da segunda linha.
                button23.Enabled = false;
            }
            if (button31.Enabled)
            { // Botão 1 da terceira linha.
                button31.Enabled = false;
            }
            if (button32.Enabled)
            { // Botão 2 da terceira linha.
                button32.Enabled = false;
            }
            if (button33.Enabled)
            { // Botão 3 da terceira linha.
                button33.Enabled = false;
            }
            if (Reiniciar.Enabled)
            {
                Reiniciar.Enabled = false;
            }
        }

        //Metodo para fazer a pontuação do placar.
        private void Placarxo()
        {

            /*
             A ideia aqui é verificar a vez do jogador, mais como já foi informado que houve um
             ganhador então ele pega e pontua quem fez a ultima jogada.
             
             Só lembrando que após ser informado que houve um ganhador todos os butôes de jogada
             são desabilitados e assim faz com que não haja um erro na pontuação.

            */
            // Verificação de jogada.
            if (VezX) // Se a vez de jogar for do "X"
            {
                placao += 1; // O jogador "O" vai ser pontuado
            }
            else if (!VezX) // Se a vez de jogar for do "O"
            {
                placax += 1; // O jogador "X" vai ser pontuado
            }
        }

            //Metodo para verificar um empate.
            private void VerificaEmpate(){
            if (
                button11.Text != string.Empty && //Se o botão 1 da linha 1 for diferente de vazio.
                button12.Text != string.Empty && //Se o botão 2 da linha 1 for diferente de vazio.
                button13.Text != string.Empty && //Se o botão 3 da linha 1 for diferente de vazio.
                button21.Text != string.Empty && //Se o botão 1 da linha 2 for diferente de vazio.
                button22.Text != string.Empty && //Se o botão 2 da linha 2 for diferente de vazio.
                button23.Text != string.Empty && //Se o botão 3 da linha 2 for diferente de vazio.
                button31.Text != string.Empty && //Se o botão 1 da linha 3 for diferente de vazio.
                button32.Text != string.Empty && //Se o botão 2 da linha 3 for diferente de vazio.
                button33.Text != string.Empty    //Se o botão 3 da linha 3 for diferente de vazio.
                )
            {
                ContadorTempo.Stop();//Para a contagem do tempo.
                TimerLeft.Text = "TEMPO: ";//Volta ao texto padrão do contador de tempo.

                Iniciar.Enabled = true;// O botão é habilitado para inicar o jogo novamente
                Reiniciar.Enabled = false; // O botão é desabilitado, sendo habilitado somente quando o jogo for inicializado.
               
                //Informa que teve um empate na jogada.
                MessageBox.Show("Houve um empate!");

            }
        


            }

        //Metodo de ação ao clicar no botão reiniciar.
        private void Reiniciar_Click(object sender, EventArgs e)
        {
            ContadorTempo.Stop();//Para a contagem do tempo.
            TimerLeft.Text = "TEMPO: ";//Volta ao texto padrão do contador de tempo.

            //Chama o metodo de reiniciar o jogo.
            ReiniciarJogo();
        }

        //Metodo de reiniciar o jogo.
        private void ReiniciarJogo()
        {
            //Vai verificar no painel tudo que for botão
            foreach (Control item in this.Controls)
            {
                //Se o item for botão então ele atribui os valores para cada botão.
                if (item is Button)
                {

                    item.Enabled = true;// Habilita os botões novamente
                    Reiniciar.Enabled = true;
                    Iniciar.Enabled = false;
                    if (!VezX)
                    {
                        buttonO.Enabled = true;
                    }
                    else
                    {
                        buttonX.Enabled = true;
                    }
                    item.Text = String.Empty;// Todos os botões ficam vazios.
                    buttonX.Text = "X";// Atribiu o nome do botão "X".
                    buttonO.Text = "O";// Atribui o nome do botão "O".

                    // Verifica quem fez a ultima jogada antes de reiniciar o jogo.
                    if (!VezX)// "!" Faz a negação da VezX, então se não for a vez do "x".
                    {
                        buttonX.Enabled = false;// Caso não for a vez do "X" ele desabilita.
                    }
                    else// Senão ele habilita o botão do "X"
                    {
                        buttonO.Enabled = false;// Caso for a vez do "X" ele desabilita o "O".
                    }
                    Reiniciar.Text = "REINICIAR";//Botão de Reiniciar recebe seu devido nome.
                    Iniciar.Text = "INICIAR";//Botãp de iniciar a rodada.

                }
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Contato com o desenvolvedor do jogo.
            System.Diagnostics.Process.Start("https://github.com/mvsviana");
        }

        //Metodo de contagem regressiva.
        private void ContadorTempo_Tick(object sender, EventArgs e)
        {
            if (Timerleft > 0) //Se o tempo for maior que zero.
            {
                Timerleft -= 1; // O tempo recebe ele menos um.
                TimerLeft.Text = Timerleft + " segundos"; // informa o novo tempo.

            }
            else // Se o tempo não for maior que zero.
            {
                ContadorTempo.Stop(); // O tempo para
                VezX = !VezX; // A vez do jogador é trocada, se for vez do "X" o "O" recebe a jogada.
                MarcaTextBTN(); // Habilitação dos botões que informa de quem é a jogada.
                TimerLeft.Text = string.Format("tempo acabou, vez do {0}", VezX ? "X" : "O"); //Label informa de quem é a jogada.
                Tempo(); // Chama o metodo de inicialização do tempo.

            }
        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            //Vai verificar no painel tudo que for botão
            foreach (Control item in this.Controls)
            {
                //Se o item for botão então ele atribui os valores para cada botão.
                if (item is Button)
                {
                    item.Enabled = true;// Habilita os botões novamente
                    Reiniciar.Enabled = true;
                    Iniciar.Enabled = false;
                    if (!VezX)
                    {
                        buttonO.Enabled = true;
                    }
                    else
                    {
                        buttonX.Enabled = true;
                    }
                    item.Text = String.Empty;// Todos os botões ficam vazios.
                    buttonX.Text = "X";// Atribiu o nome do botão "X".
                    buttonO.Text = "O";// Atribui o nome do botão "O".

                    // Verifica quem fez a ultima jogada antes de reiniciar o jogo.
                    if (!VezX)// "!" Faz a negação da VezX, então se não for a vez do "x".
                    {
                        buttonX.Enabled = false;// Caso não for a vez do "X" ele desabilita.
                    }
                    else// Senão ele habilita o botão do "X"
                    {
                        buttonO.Enabled = false;// Caso for a vez do "X" ele desabilita o "O".
                    }
                    Reiniciar.Text = "REINICIAR";//Botão de Reiniciar recebe seu devido nome.
                    Iniciar.Text = "INICIAR";//Botãp de iniciar a rodada.

                }
            }
        }

        //Metodo de inicialização de tempo por jogada.
        private void Tempo()
        {
            Timerleft = 15; // É inicializado com 15 segundos por jogada.
            TimerLeft.Text = "15 segundos"; // Label informa o tempo
            ContadorTempo.Start(); // Tempo se inicializa.
        }
    }
}
