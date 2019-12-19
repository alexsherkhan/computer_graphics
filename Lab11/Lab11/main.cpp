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

	// Street lights
	const GLfloat light_ambient[] = { 0.0, 0.0, 0.0, 1 };
	const GLfloat light_diffuse[] = { 1.0, 1.0, 1.0, 1.0 };
	const GLfloat light_specular[] = { 1.0, 1.0, 1.0, 1.0 };
	glLightfv(GL_LIGHT1, GL_AMBIENT, light_ambient);
	glLightfv(GL_LIGHT1, GL_DIFFUSE, light_diffuse);
	glLightfv(GL_LIGHT1, GL_SPECULAR, light_specular);

	glLightfv(GL_LIGHT2, GL_AMBIENT, light_ambient);
	glLightfv(GL_LIGHT2, GL_DIFFUSE, light_diffuse);
	glLightfv(GL_LIGHT2, GL_SPECULAR, light_specular);

	glLightfv(GL_LIGHT3, GL_AMBIENT, light_ambient);
	glLightfv(GL_LIGHT3, GL_DIFFUSE, light_diffuse);
	glLightfv(GL_LIGHT3, GL_SPECULAR, light_specular);

	glEnable(GL_DEPTH_TEST);
	glEnable(GL_LIGHTING);

	makeTextureImage();

	// Прожектор камеры 
	glLightfv(GL_LIGHT0, GL_DIFFUSE, light_diffuse);
	glLightf(GL_LIGHT0, GL_SPOT_CUTOFF, 30.0);
	glLightf(GL_LIGHT0, GL_LINEAR_ATTENUATION, 0.1);
	glLightf(GL_LIGHT0, GL_QUADRATIC_ATTENUATION, 0.01);


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

void drawLights()
{
	const GLfloat no_mat[] = { 0.0, 0.0, 0.0, 1.0 };
	const GLfloat mat_emission[] = { 1, 1, 1, 0.0 };
	const GLfloat position[] = { 0, 4, 0, 1 };
	const GLfloat car_mat_emission[] = { 1, 1, 0, 0.0 };
	glColor3f(1.0, 1.0, 1.0);

	// lamps
	glPushMatrix();
	glTranslatef(-5, 0, 0);
	glLightfv(GL_LIGHT1, GL_POSITION, position);
	glTranslatef(5, 0, -10);
	glLightfv(GL_LIGHT2, GL_POSITION, position);
	glTranslatef(3, 0, 13);
	glLightfv(GL_LIGHT3, GL_POSITION, position);
	glPopMatrix();

	// lamps
	glPushMatrix();
	if (glIsEnabled(GL_LIGHT1))
		glMaterialfv(GL_FRONT, GL_EMISSION, mat_emission);
	else glMaterialfv(GL_FRONT, GL_EMISSION, no_mat);
	glColor3f(1.0, 1.0, 0.0);
	glTranslatef(-5, 4, 0);
	glutSolidCube(0.1);
	glTranslatef(5, 0, -10);
	if (glIsEnabled(GL_LIGHT2))
		glMaterialfv(GL_FRONT, GL_EMISSION, mat_emission);
	else glMaterialfv(GL_FRONT, GL_EMISSION, no_mat);
	glutSolidCube(0.1);
	glTranslatef(3, 0, 13);
	if (glIsEnabled(GL_LIGHT3))
		glMaterialfv(GL_FRONT, GL_EMISSION, mat_emission);
	else glMaterialfv(GL_FRONT, GL_EMISSION, no_mat);
	glutSolidCube(0.1);
	glColor3f(1.0, 1.0, 1.0);
	glMaterialfv(GL_FRONT, GL_EMISSION, no_mat);
	glPopMatrix();

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

void drawLamps()
{
	glColor3f(0.5, 0.5, 0.5);

	//1 фонарь
	glPushMatrix();
	glTranslatef(-5, 0, 0);
	glRotatef(-90, 1, 0, 0);
	glutSolidCone(0.2, 4, 10, 10);
	glTranslatef(0, 0.1, 4);
	glRotatef(90, 1, 0, 0);
	glutSolidCone(0.15, 1, 10, 10);
	glPopMatrix();

	//2 фонарь
	glPushMatrix();
	glTranslatef(0, 0, -10);
	glRotatef(-90, 1, 0, 0);
	glutSolidCone(0.2, 4, 10, 10);
	glTranslatef(0, 0.1, 4);
	glRotatef(90, 1, 0, 0);
	glutSolidCone(0.15, 1, 10, 10);
	glPopMatrix();

	//3 фонарь
	glPushMatrix();
	glTranslatef(3, 0, 3);
	glRotatef(-90, 1, 0, 0);
	glutSolidCone(0.2, 4, 10, 10);
	glTranslatef(0, 0.1, 4);
	glRotatef(90, 1, 0, 0);
	glutSolidCone(0.15, 1, 10, 10);
	glPopMatrix();
}

// Отображение
void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glMatrixMode(GL_MODELVIEW);
	
	drawLights();

	drawRoad();

	drawLamps();
	
	glutSwapBuffers();
}

// Реакция на клавиатуру
void specialKeys(int key, int x, int y)
{
	switch (key)
	{
	case GLUT_KEY_UP: camera_pos += 0.5; break;
	case GLUT_KEY_DOWN: camera_pos -= 0.5; break;
	case GLUT_KEY_RIGHT: camera_angle -= 3; break;
	case GLUT_KEY_LEFT: camera_angle += 3; break;
	case GLUT_KEY_PAGE_UP: camera_rad -= 0.5; break;
	case GLUT_KEY_PAGE_DOWN: camera_rad += 0.5; break;
	case GLUT_KEY_F1:
		if (glIsEnabled(GL_LIGHT1))
			glDisable(GL_LIGHT1);
		else glEnable(GL_LIGHT1);
		break;
	case GLUT_KEY_F2:
		if (glIsEnabled(GL_LIGHT2))
			glDisable(GL_LIGHT2);
		else glEnable(GL_LIGHT2);
		break;
	case GLUT_KEY_F3:
		if (glIsEnabled(GL_LIGHT3))
			glDisable(GL_LIGHT3);
		else glEnable(GL_LIGHT3);
		break;
	case GLUT_KEY_F10:
		if (glIsEnabled(GL_LIGHT0))
			glDisable(GL_LIGHT0);
		else glEnable(GL_LIGHT0);
		break;
	}
	setCamera();
	glutPostRedisplay();
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
	glutSpecialFunc(specialKeys);

	glutMainLoop();
	return 0;
}

