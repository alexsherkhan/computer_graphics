#include <windows.h>
#include<stdio.h>
#include <GL/glew.h>
#include <GL/freeglut.h>
#include <ctime>
#include <vector>
#include<conio.h>
#include<math.h>
#include<string.h>
#include <SOIL.h>

using std::vector;

GLuint texture;

double pi = 3.14159265358979323846;

float camera_angle = 50;
float camera_pos = 5;
float camera_rad = 10;
float car_angle = 0;
int width = 0, height = 0;

void makeTextureImage()
{
	texture = SOIL_load_OGL_texture
	(
		"paving.jpg",
		SOIL_LOAD_AUTO,
		SOIL_CREATE_NEW_ID,
		SOIL_FLAG_MIPMAPS | SOIL_FLAG_INVERT_Y | SOIL_FLAG_NTSC_SAFE_RGB | SOIL_FLAG_COMPRESS_TO_DXT
	);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_NEAREST);
	glTexParameteri(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_NEAREST);
	
}


// Инициализация
void init(void)
{
	glClearColor(0.0, 0.0, 0.0, 0.0);
	glEnable(GL_NORMALIZE);
	glEnable(GL_COLOR_MATERIAL);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);

	makeTextureImage();



}

double gr_cos(float angle) noexcept
{
	return cos(angle / 180 * pi);
}

double gr_sin(float angle) noexcept
{
	return sin(angle / 180 * pi);
}


void setCamera()
{
	glLoadIdentity();
	const double x = camera_rad * gr_cos(camera_angle);
	const double z = camera_rad * gr_sin(camera_angle);
	gluLookAt(x, camera_pos, z, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);

	const GLfloat spot_position[] = { x, camera_pos, z , 1 };
	GLfloat spot_direction[] = { -x, -camera_pos, -z };
	const float length = sqrt(x * x + static_cast<double>(camera_pos) * static_cast<double>(camera_pos) + z * z);
	if (length != 0)
	{
		spot_direction[0] /= length;
		spot_direction[1] /= length;
		spot_direction[2] /= length;
	}
	glLightfv(GL_LIGHT0, GL_SPOT_DIRECTION, spot_direction);
	glLightfv(GL_LIGHT0, GL_POSITION, spot_position);
}

// Изменение размеров окна
void reshape(int w, int h)
{
	width = w; height = h;
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluPerspective(60.0, static_cast<GLfloat>(w) / static_cast<GLfloat>(h), 1.0, 100.0);
	glMatrixMode(GL_MODELVIEW);
	glLoadIdentity();
	setCamera();
}



void drawRoad()
{
	glColor3f(1, 1, 1);

	glBindTexture(GL_TEXTURE_2D, texture);
	glEnable(GL_TEXTURE_2D);
	glBegin(GL_QUADS);
	const float delta = 0.25;
	const float texture_delta = 0.125;
	int kk = 0;
	float ff = texture_delta;
	for (float i = -40; i < 40; i += delta)
	{
		int k = 0;
		float f = texture_delta;
		for (float j = -40; j < 40; j += delta)
		{
			glNormal3f(0, 1, 0); glTexCoord2f(ff - texture_delta, f - texture_delta); glVertex3f(i - delta, 0, j - delta);
			glNormal3f(0, 1, 0); glTexCoord2f(ff - texture_delta, f); glVertex3f(i - delta, 0, j);
			glNormal3f(0, 1, 0); glTexCoord2f(ff, f); glVertex3f(i, 0.0, j);
			glNormal3f(0, 1, 0); glTexCoord2f(ff, f - texture_delta); glVertex3f(i, 0.0, j - delta);
			++k;
			f = k % 8 == 0 ? texture_delta : f + texture_delta;
		}
		++kk;
		ff = kk % 8 == 0 ? texture_delta : ff + texture_delta;
	}
	glEnd();
	glDisable(GL_TEXTURE_2D);
}


// Отображение
void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glMatrixMode(GL_MODELVIEW);
	
	drawRoad();
	
	glutSwapBuffers();
}

int main(int argc, char * argv[])
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowSize(800, 800);
	glutInitWindowPosition(10, 10);
	glutCreateWindow("Lab 11");
	init();
	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	
	glutMainLoop();
	return 0;
}

