using namespace System::Drawing::Drawing2D;
using namespace System::Threading;

// declare globals to simplify student code
extern Graphics *currentg;

// Pens and Brushes for drawing. 
extern Pen   *grndPen;			// pen for drawing ground
extern Pen   *cannonPen;		// pen for drawing cannon barrel
extern Brush *ballBrush;		// brush for filling cannon ball
extern Brush *bldgBrush;		// brush for creating building pattern
extern Brush *ballErase;		// brush to erase ball

void drawCannon(int basex, int basey, int tipx, int tipy)
{
	currentg->DrawLine(cannonPen,basex, basey, tipx, tipy);
}