#include <windows.h>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <ctime>
#include <vector>

using std::vector;

struct Color
{
	float R;
	float G;
	float B;
};

struct Tuple
{
	double first;
	double second;
	double third;

	Tuple(double f, double s, double t) : first(f), second(s), third(t) {}
};



double rotateX = 0;
double rotateY = 0;
double rotateZ = 0;

static Color color;
int rotate_mode = 0;
static int w = 0, h = 0;

// Первая показанная картинка (стандартный примитив : куб + 2 треугольника + четырехугольник)
bool firstShow = true;
bool GSBMode = false;
bool treeMode = false;
bool isPerspective = false;
// Индекс примитива в векторе, который нужно выводить 
int index = 0;
// Ф-ия вызываемая перед вхождением в главный цикл
void init(void)
{
	glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
	color.R = color.G = color.B = 0.0f;
}

// Ф-ия получения рандомного цвета
void changeColor()
{
	color.R = rand() % 256 / 255.0;
	color.G = rand() % 256 / 255.0;
	color.B = rand() % 256 / 255.0;
}

// Ф-ия построения прямоугольника (сине-голубой градиент)
void rectangle()
{
	glBegin(GL_QUADS);
	glColor3f(0.0, 1.0, 1.0);
	glVertex2f(-0.1f, -0.1f);
	glVertex2f(-0.1f, 0.1f);
	glColor3f(0.0, 0.0, 1.0);
	glVertex2f(0.1f, 0.1f);
	glVertex2f(0.1f, -0.1f);
	glEnd();
}

// Ф-ии построения стандартных примитивов:

// Ф-ия построения куба
void solidCube()
{
	glutSolidCube(0.5);
}

// Ф-ия построения каркаса куба
void wireCube()
{
	glutWireCube(0.5);
}

// Ф-ия построения каркаса чайника
void wireTeapot()
{
	glutWireTeapot(0.5);
}

// Ф-ия построения каркаса тора
void wireTorus()
{
	glutWireTorus(0.3, 0.5, 4, 5);
}

// Ф-ия построения каркаса тетраэдра 
void wireTetrahedron()
{
	glutWireTetrahedron();
}

// Ф-ия построения каркаса икосаэдра
void wireIcosahedron()
{
	glutWireIcosahedron();
}

// Ф-ия построения треугольника
void triangle()
{
	glBegin(GL_TRIANGLES);
	glColor3f(0.0, 0.0, 0.9);
	glVertex2f(0.25f, 0.25f);
	glVertex2f(-0.25f, 0.25f);
	glVertex2f(0.0f, 0.5f);
	glEnd();
}


// Ф-ия построения треугольника, окрашенного в различные цвета
void triangleWithDifferentVertex()
{
	glBegin(GL_TRIANGLES);
	glColor3f(1.0, 0.0, 0.0);	glVertex2f(0.25f, -0.25f);
	glColor3f(0.0, 1.0, 0.0);	glVertex2f(-0.25f, -0.25f);
	glColor3f(0.0, 0.0, 1.0);	glVertex2f(0.0f, -0.5f);
	glEnd();
}
  
void Draw_GSB()
{
	// clearing the window or remove all drawn objects    
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);


	if (!isPerspective)
	{
		glViewport(0, 0, w, h);
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		gluPerspective(65.0, w / h, 1.0, 1000.0);
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
		gluLookAt(-0.3, 0.5, 5.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);
	}
	else
	{
		glViewport(0, 0, w, h);
		glMatrixMode(GL_PROJECTION);
		glLoadIdentity();
		glOrtho(-2, 2, -2, 2, -2, 2);
		glMatrixMode(GL_MODELVIEW);
		glLoadIdentity();
	}
	glTranslatef(0, 0.0, 0.0);
	glColor3f(1, 1, 1);
	glutSolidCube(0.1);

	if (rotate_mode == 0)
	{
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
	}

	//gold
	glPushMatrix();
	glTranslatef(1, 0.0, 1.0);
	if (rotate_mode)
	{
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
	}

	glColor3f(1, 0.78, 0.09);
	float mat_specular[] = { 1,1,1,1 };
	glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, mat_specular);
	glMaterialf(GL_FRONT_AND_BACK, GL_SHININESS, 128.0);
	glScalef(1, 2, 1);
	glutSolidCube(0.5);
	glPopMatrix();

	//silver
	glPushMatrix();
	if (rotate_mode == 1)
	{
		glTranslatef(1, 0.0, 1.0);
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
		glTranslatef(-1, 0.0, -1.0);
	}
	glTranslatef(0.25, -0.125, 1.0);
	if (rotate_mode == 2)
	{
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
	}
	glColor3f(0.75, 0.75, 0.75);
	glScalef(1, 1.5, 1);
	glutSolidCube(0.5);
	glPopMatrix();

	//bronze
	glPushMatrix();
	if (rotate_mode == 1)
	{
		glTranslatef(1, 0.0, 1.0);
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
		glTranslatef(-1, 0.0, -1.0);
	}
	glTranslatef(1.7, -0.25, 1.0);
	if (rotate_mode == 2)
	{
		glRotatef(rotateX, 1, 0, 0);
		glRotatef(rotateY, 0, 1, 0);
		glRotatef(rotateZ, 0, 0, 1);
	}
	glColor3f(0.8, 0.5, 0.2);
	glutSolidCube(0.5);
	glPopMatrix();

}

void drawChristmasTree(double scale = 0.5)
{
	glPushMatrix();

	glScaled(scale, scale, scale);
	glRotated(-90, 1, 0, 0);
	glColor3d(0.9, 0.6, 0.3);
	glutSolidCylinder(0.1, 0.3, 32, 32);

	glTranslated(0, 0, 0.3);
	glColor3d(0, 1, 0);
	glutSolidCone(0.4, 0.3, 32, 32);
	glTranslated(0, 0, 0.2);
	glScaled(0.7, 0.7, 0.7);
	glutSolidCone(0.4, 0.3, 32, 32);
	glTranslated(0, 0, 0.2);
	glScaled(0.7, 0.7, 0.7);
	glutSolidCone(0.4, 0.3, 32, 32);
	glPopMatrix();
}

void treeForest()
{
	vector<Tuple> trees = {
		Tuple(0.5, 0.5, 0.5),
			Tuple(0.8, 0.8, 0.3),
			Tuple(0.8, 0.3, 0.4),
			Tuple(0.2, 0.3, 0.4),
			Tuple(0.1, 0.1, 0.2),
			Tuple(0.9, 0.1, 0.3),
			Tuple(0.5, 0.1, 0.2),
			Tuple(0.7, 0.1, 0.2),
			Tuple(0.3, 0.1, 0.2),
	};
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	if (isPerspective) gluPerspective(60, w * 1.0 / h, 0.1, 100);
	else glOrtho(-1, 1, -1, 1, -100, 100);

	glMatrixMode(GL_MODELVIEW);
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();

	glTranslated(0, 0, -1);
	glRotated(20, 1, 0, 0);
	glRotated(0, 0, 1, 0);
	glTranslated(0.5, 0, 0.5);

	for each (Tuple tree in trees)
	{
		glPushMatrix();
		glTranslated(-tree.first, 0, -tree.second);
		drawChristmasTree(tree.third);
		glPopMatrix();
	}
	glFlush();
}

typedef void(*callback_t)(void);
vector<callback_t> allPrimitives = { solidCube, wireCube, wireTeapot, wireTorus,
wireTetrahedron, wireIcosahedron };


// Ф-ия изменения примитива по щелчку мыши
void mouseChangePrimitive(int button, int state, int x, int y)
{
	if (button == GLUT_LEFT_BUTTON && state == GLUT_DOWN)
	{
		firstShow = false;
		changeColor();
		index = rand() % allPrimitives.size();
	}
}

// Управление клавиатурой
void specialKeys(int key, int x, int y)
{
	switch (key)
	{
	case GLUT_KEY_UP: rotateX += 5; break;
	case GLUT_KEY_DOWN: rotateX -= 5; break;
	case GLUT_KEY_RIGHT: rotateY += 5; break;
	case GLUT_KEY_LEFT: rotateY -= 5; break;
	case GLUT_KEY_PAGE_UP: rotateZ += 5; break;
	case GLUT_KEY_PAGE_DOWN: rotateZ -= 5; break;
	case GLUT_KEY_CTRL_L: treeMode = !treeMode; break;
	case GLUT_KEY_CTRL_R: isPerspective = !isPerspective; break;
	case GLUT_KEY_F1:
		rotateX = rotateY = rotateZ = 0;
		rotate_mode = 0; break;
	case GLUT_KEY_F2:
		rotateX = rotateY = rotateZ = 0; rotate_mode = 1; break;
	case GLUT_KEY_F3:
		rotateX = rotateY = rotateZ = 0; rotate_mode = 2; break;
	case GLUT_KEY_F4:
		GSBMode =!GSBMode; break;
	}
	glutPostRedisplay();
}


// Ф-ия, вызываемая каждый кадр
void update()
{
	glClear(GL_COLOR_BUFFER_BIT);
	glClearColor(0.0f, 0.0f, 0.0f, 0.0f);

	glLoadIdentity();
	glRotatef(rotateX, 1.0, 0.0, 0.0);
	glRotatef(rotateY, 0.0, 1.0, 0.0);
	glRotatef(rotateZ, 0.0, 0.0, 1.0);

	if (firstShow)
	{
		glColor3f(1.0, 0.26, 0.23);
		solidCube();
		triangle();
		rectangle();
		triangleWithDifferentVertex();
	}
	if (treeMode) {
		treeForest();
	}
	if (GSBMode) {
		Draw_GSB();
	}
	else
	{
		// Рисование примитива по щелчку мыши
		glColor3f(color.R, color.G, color.B);
		allPrimitives[index]();
	}

	glFlush();
	glutSwapBuffers();
}

// Ф-ия, вызываемая при изменении размера окна
void reshape(int width, int height)
{
	w = width;
	h = height;
}

int main(int argc, char * argv[])
{
	srand(time(0));
	glutInit(&argc, argv);
	glutInitWindowPosition(100, 100);
	glutInitWindowSize(800, 600);
	glutInitDisplayMode(GLUT_RGBA | GLUT_DOUBLE);
	glutCreateWindow("Lab10");
	glutDisplayFunc(update);
	glutReshapeFunc(reshape);
	glutMouseFunc(mouseChangePrimitive);
	glutSpecialFunc(specialKeys);
	init();
	glutMainLoop();
	return 0;
}

