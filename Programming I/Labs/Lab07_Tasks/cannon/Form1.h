#pragma once
using namespace System::Drawing;
using namespace System::Threading;
using namespace System::Drawing::Drawing2D;

namespace cannon
{
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary> 
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the 
	///          'Resource File Name' property for the managed resource compiler tool 
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public __gc class Form1 : public System::Windows::Forms::Form
	{	
	public:
		Form1(void)
		{
			InitializeComponent();
		}
  
	protected:
		void Dispose(Boolean disposing)
		{
			if (disposing && components)
			{
				components->Dispose();
			}
			__super::Dispose(disposing);
		}
	private: System::Windows::Forms::GroupBox *  groupBox1;
	private: System::Windows::Forms::TextBox *  angleBox;
	private: System::Windows::Forms::Label *  label1;

	private: System::Windows::Forms::Label *  label2;

	private: System::Windows::Forms::Panel *  panel1;
	private: System::Windows::Forms::TextBox *  speedBox;
	private: System::Windows::Forms::Button *  drawBtn;

			 // for cannon
	private:	Graphics *currentg;

// Pens and Brushes for drawing. 
private:Pen   *grndPen;			// pen for drawing ground
private:Pen   *cannonPen;		// pen for drawing cannon barrel
private:Brush *ballBrush;		// brush for filling cannon ball
private:Brush *bldgBrush;		// brush for creating building pattern
private:Brush *ballErase;		// brush to erase ball

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container * components;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->groupBox1 = new System::Windows::Forms::GroupBox();
			this->label2 = new System::Windows::Forms::Label();
			this->speedBox = new System::Windows::Forms::TextBox();
			this->label1 = new System::Windows::Forms::Label();
			this->angleBox = new System::Windows::Forms::TextBox();
			this->drawBtn = new System::Windows::Forms::Button();
			this->panel1 = new System::Windows::Forms::Panel();
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// groupBox1
			// 
			this->groupBox1->Controls->Add(this->label2);
			this->groupBox1->Controls->Add(this->speedBox);
			this->groupBox1->Controls->Add(this->label1);
			this->groupBox1->Controls->Add(this->angleBox);
			this->groupBox1->Location = System::Drawing::Point(248, 520);
			this->groupBox1->Name = S"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(192, 136);
			this->groupBox1->TabIndex = 0;
			this->groupBox1->TabStop = false;
			this->groupBox1->Text = S"Input Parameters";
			// 
			// label2
			// 
			this->label2->Location = System::Drawing::Point(40, 80);
			this->label2->Name = S"label2";
			this->label2->Size = System::Drawing::Size(48, 16);
			this->label2->TabIndex = 3;
			this->label2->Text = S"Speed:";
			// 
			// speedBox
			// 
			this->speedBox->Location = System::Drawing::Point(88, 80);
			this->speedBox->Name = S"speedBox";
			this->speedBox->Size = System::Drawing::Size(72, 22);
			this->speedBox->TabIndex = 2;
			this->speedBox->Text = S"50";
			// 
			// label1
			// 
			this->label1->Location = System::Drawing::Point(24, 40);
			this->label1->Name = S"label1";
			this->label1->Size = System::Drawing::Size(40, 16);
			this->label1->TabIndex = 1;
			this->label1->Text = S"Angle:";
			// 
			// angleBox
			// 
			this->angleBox->Location = System::Drawing::Point(88, 40);
			this->angleBox->Name = S"angleBox";
			this->angleBox->Size = System::Drawing::Size(72, 22);
			this->angleBox->TabIndex = 0;
			this->angleBox->Text = S"45";
			// 
			// drawBtn
			// 
			this->drawBtn->Location = System::Drawing::Point(480, 560);
			this->drawBtn->Name = S"drawBtn";
			this->drawBtn->Size = System::Drawing::Size(176, 48);
			this->drawBtn->TabIndex = 1;
			this->drawBtn->Text = S"Click Here to see animation";
			this->drawBtn->Click += new System::EventHandler(this, &cannon::Form1::drawBtn_Click);
			// 
			// panel1
			// 
			this->panel1->BorderStyle = System::Windows::Forms::BorderStyle::FixedSingle;
			this->panel1->Location = System::Drawing::Point(8, 16);
			this->panel1->Name = S"panel1";
			this->panel1->Size = System::Drawing::Size(952, 488);
			this->panel1->TabIndex = 2;
			// 
			// Form1
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(6, 15);
			this->ClientSize = System::Drawing::Size(976, 672);
			this->Controls->Add(this->panel1);
			this->Controls->Add(this->drawBtn);
			this->Controls->Add(this->groupBox1);
			this->Name = S"Form1";
			this->Text = S"Cannon Simulation";
			this->groupBox1->ResumeLayout(false);
			this->ResumeLayout(false);

		}	

	private: System::Void drawBtn_Click(System::Object *  sender, System::EventArgs *  e)
			 {
				 Graphics *g = panel1->CreateGraphics();

				 String *angleText = angleBox->Text;
				 String *speedText = speedBox->Text;

				 int angle, speed;

				 try
				 {
					angle = Convert::ToInt32( angleText );
					speed = Convert::ToInt32( speedText );
					// erase panel (to allow re-test)
					// find size of panel and draw rectangle
					// with background color
					Color backColor = panel1->BackColor;
					// paint rectangle with background color
					// of panel
					SolidBrush *eraseBrush = new SolidBrush(backColor);
					g->FillRectangle(eraseBrush,0,0,panel1->Width, panel1->Height);
					
					animate(g, speed, angle);
				 }
				 catch( FormatException *)
				 {
					 MessageBox::Show("Invalid or empty input values!");
				 }
			 }

public:

	void drawCannon(int basex, int basey, int tipx, int tipy)
	{
		currentg->DrawLine(cannonPen,basex, basey, tipx, tipy);
	}
	void shoot(int speed, int angle);
	void drawBall(int ballx, int bally)
	{
		const int   BALL_RADIUS = 4;	// radius of cannon ball
		currentg->FillEllipse(ballBrush, 
							  ballx - BALL_RADIUS, 
							  bally - BALL_RADIUS, 
							  BALL_RADIUS*2, BALL_RADIUS*2);
	}
	void delay(int millisec)
	{
		Thread::Sleep(millisec);
	}

	void eraseBall(int ballx, int bally)
	{
		const int   BALL_RADIUS = 4;	// radius of cannon ball
		currentg->FillEllipse(ballErase, 
							  ballx - BALL_RADIUS, 
							  bally - BALL_RADIUS, 				
							  BALL_RADIUS*2, BALL_RADIUS*2);
	}

	void animate(Graphics *g, int speed, int angle)
{
	currentg = g;
	// create pen to use for drawing ground

	// position and length of line repesenting ground
	const int GROUND_Y = 450;
	const int GROUND_X = 10;
	const int GROUND_SIZE = 900;
	const int GROUND_THICK = 10;

	// position and size of "building"
	const int BLDG_HEIGHT = 100;
	const int BLDG_WIDTH  = 50;
	const int BLDG_OFFSET = 700;
	const int BLDG_LEFT = GROUND_X + BLDG_OFFSET;
	const int BLDG_TOP = GROUND_Y - GROUND_THICK/2 - BLDG_HEIGHT;
	const int BLDG_RIGHT = BLDG_LEFT + BLDG_WIDTH;

	// misc constants affecting animation
	const float GRAVITY = 9.8f;		// force of gravity
	const int   BALL_RADIUS = 4;	// radius of cannon ball
	const float DELTA_TIME = .10f;	// time increment
	const int   TIME_LIMIT = 20;    // animation loop limit

	// starting position of line representing cannon barrel
	// This is the location of the "base" of the barrel
	const int CANNON_BASE_X = GROUND_X + 50;
	const int CANNON_BASE_Y = GROUND_Y - GROUND_THICK/2;

	// cannon barrel length and thickness
	const int CANNON_LEN = 25;
	const int CANNON_THICK = 2 * BALL_RADIUS;

	grndPen = new Pen(Color::Green, static_cast<float>(GROUND_THICK));
	
	// create brushes for drawing building and cannon ball
	bldgBrush = new HatchBrush(HatchStyle::HorizontalBrick, Color::Firebrick);
	ballBrush = new SolidBrush(Color::Black);
	ballErase = new SolidBrush(panel1->BackColor);

	// create pen for drawing cannon
	cannonPen = new Pen(Color::Black, static_cast<float>(CANNON_THICK));

	
	// create pen to use for drawing ground
	grndPen = new Pen(Color::Green, static_cast<float>(GROUND_THICK));
	
	// create brushes for drawing building and cannon ball
	bldgBrush = new HatchBrush(HatchStyle::HorizontalBrick, Color::Firebrick);
	ballBrush = new SolidBrush(Color::Black);
	ballErase = new SolidBrush(panel1->BackColor);

	// create pen for drawing cannon
	cannonPen = new Pen(Color::Black, static_cast<float>(CANNON_THICK));

	// draw ground
	g->DrawLine(grndPen, GROUND_X, GROUND_Y, 
		        GROUND_X + GROUND_SIZE, GROUND_Y);  
	
	// draw building
	g->FillRectangle(bldgBrush,BLDG_LEFT, BLDG_TOP, BLDG_WIDTH, BLDG_HEIGHT);

	shoot(speed, angle);

}


};
}


