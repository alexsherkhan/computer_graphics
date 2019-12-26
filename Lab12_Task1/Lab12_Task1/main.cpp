#include<Windows.h>    
// first include Windows.h header file which is required    
#include<stdio.h>
#include "GL/glew.h"
#include<gl/GL.h>   // GL.h header file    
#include<gl/GLU.h> // GLU.h header file    
#include<gl/glut.h>  // glut.h header file from freeglut\include\GL folder    
#include<conio.h>
#include<math.h>
#include<string>
#include <vector>
#include <string>
#include <sstream>
#include <fstream>
#include <iostream>

int width = 0, height = 0;

GLuint Program;
GLint Attrib_vertex, Unif_color;
GLint Unif_angle;
GLint Unif_rotationX;
GLint Unif_rotationY;
GLint Unif_rotationZ;
GLint Unif_scale;;

float angle_x = 90.0f, angle_y = 90.0f, angle_z = 90.0f;
int axis = 0;
float rotate_x = 0;
float rotate_y = 0;
float rotate_z = 0;

float scale_x = 1, scale_y = 1, scale_z = 1;
//0 - scale, 1 - rotation
int mode = 0;
void checkOpenGLerror() {
	GLenum errCode;
	if ((errCode = glGetError()) != GL_NO_ERROR)
		std::cout << "OpenGl error! - " << gluErrorString(errCode);
}

std::string readfile(const char* path)
{
	std::string res = "";
	std::ifstream file(path);
	std::string line;
	getline(file, res, '\0');
	while (getline(file, line))
	{
		res += "\n " + line;
	}
	return res;
}

static void checkScaleBounds() {
	if (scale_x < 0.1f) scale_x = 0.1f;
	if (scale_y < 0.1f) scale_y = 0.1f;
	if (scale_z < 0.1f) scale_z = 0.1f;
}


void initShader()
{
	
	std::string readed = readfile("vertex.shader");
	const char* vsSource = readed.c_str();

	std::string readed2 = readfile("fragment.shader");
	const char* fsSource = readed2.c_str();

	GLuint vShader, fShader;
	vShader = glCreateShader(GL_VERTEX_SHADER);
	glShaderSource(vShader, 1, &vsSource, NULL);
	glCompileShader(vShader);

	fShader = glCreateShader(GL_FRAGMENT_SHADER);
	glShaderSource(fShader, 1, &fsSource, NULL);
	glCompileShader(fShader);

	Program = glCreateProgram();
	glAttachShader(Program, vShader);
	glAttachShader(Program, fShader);
	glLinkProgram(Program);

	int link_ok;
	glGetProgramiv(Program, GL_LINK_STATUS, &link_ok);
	if (!link_ok)
	{
		std::cout << "error attach shaders \n";
		return;
	}

	const char* attr_name = "coord";
	Attrib_vertex = glGetAttribLocation(Program, attr_name);
	if (Attrib_vertex == -1)
	{
		std::cout << "could not bind attrib " << attr_name << std::endl;
		return;
	}

	const char* color_name = "color";
	Unif_color = glGetUniformLocation(Program, color_name);
	if (Unif_color == -1)
	{
		std::cout << "could not bind uniform " << color_name << std::endl;
		return;
	}

	////! Вытягиваем ID юниформ
	char *unif_name = "rotationX";
	Unif_rotationX = glGetUniformLocation(Program, unif_name);
	if (Unif_rotationX == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	unif_name = "rotationY";
	Unif_rotationY = glGetUniformLocation(Program, unif_name);
	if (Unif_rotationY == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	unif_name = "rotationZ";
	Unif_rotationZ = glGetUniformLocation(Program, unif_name);
	if (Unif_rotationZ == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}
	unif_name = "scale";
	Unif_scale = glGetUniformLocation(Program, unif_name);
	if (Unif_scale == -1)
	{
		std::cout << "could not bind uniform " << unif_name << std::endl;
		return;
	}

	checkOpenGLerror();
}

void freeShader()
{
	glUseProgram(0);
	glDeleteProgram(Program);
}

void init(void)
{
	glClearColor(0.0, 0.0, 0.0, 0.0);

	glEnable(GL_DEPTH_TEST);
}

void reshape(int w, int h)
{
	width = w; height = h;
	glViewport(0, 0, w, h);
}

void rotate_matrix()
{
	float angleX = 3.14f * rotate_x / 180.0f;
	float rotateX[] = {
		1, 0, 0, 0,
		0, cos(angleX), sin(angleX), 0,
		0, -sin(angleX), cos(angleX), 0,
		0, 0, 0, 1 };
	glUniformMatrix4fv(Unif_rotationX, 1, false, rotateX);

	float angleY = 3.14f * rotate_y / 180.0f;
	float rotateY[] = {
		cos(angleY), 0, -sin(angleY), 0,
		0, 1, 0, 0,
		sin(angleY), 0, cos(angleY), 0,
		0, 0, 0, 1 };
	glUniformMatrix4fv(Unif_rotationY, 1, false, rotateY);

	float angleZ = 3.14f * rotate_z / 180.0f;
	float rotateZ[] = {
		cos(angleZ), sin(angleZ), 0, 0,
		-sin(angleZ), cos(angleZ), 0, 0,
		0, 0, 1, 0,
		0, 0, 0, 1 };
	glUniformMatrix4fv(Unif_rotationZ, 1, false, rotateZ);


}
void display(void)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
	glLoadIdentity();
	glUseProgram(Program);
	static float red[4] = { 0.0f, 0.0f, 1.0f, 1.0f };
	glUniform4fv(Unif_color, 1, red);

	rotate_matrix();
	checkScaleBounds();
	float scale[] = { scale_x, 0.0f,0.0f, 0.0f,
		0.0f , scale_y, 0.0f, 0.0f ,
		0.0f ,0.0f , scale_z, 0.0f ,
		0.0f ,0.0f ,0.0f, 1.0f };
	glUniformMatrix4fv(Unif_scale, 1, false, scale);

	glBegin(GL_QUADS);
	glColor3f(1.0, 0.0, 0.0); glVertex2f(-0.5f, -0.5f);
	glColor3f(0.0, 1.0, 0.0); glVertex2f(-0.5f, 0.5f);
	glColor3f(0.0, 0.0, 1.0); glVertex2f(0.5f, 0.5f);
	glColor3f(1.0, 1.0, 1.0); glVertex2f(0.5f, -0.5f);
	glEnd();

	glutSolidCube(1);
	glFlush();
	//! Отключаем шейдерную программу
	glUseProgram(GL_ZERO);
	checkOpenGLerror();
	glutSwapBuffers();


}

void special(int key, int x, int y)
{
	switch (key)
	{
	case GLUT_KEY_F1: mode = 0;
		break;
	case GLUT_KEY_F2: mode = 1;
		break;
	}
}

void keyboard(unsigned char key, int x, int y)
{
	if (mode == 0)
	{
		switch (key)
		{
		case '1': scale_x += 0.1;
			break;
		case '2': scale_x -= 0.1;
			break;
		case '3': scale_y += 0.1;
			break;
		case '4': scale_y -= 0.1;
			break;
		case '5': scale_z += 0.1;
			break;
		case '6': scale_z -= 0.1;
			break;
		}
	}
	else

		switch (key)
		{
		case '1': rotate_x += 5;
			break;
		case '2': rotate_x -= 5;
			break;
		case '3': rotate_y += 5;
			break;
		case '4': rotate_y -= 5;
			break;
		case '5': rotate_z += 5;
			break;
		case '6': rotate_z -= 5;
			break;
		}
	glutPostRedisplay();
}

int main(int argc, char **argv)
{
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_DEPTH | GLUT_DOUBLE | GLUT_RGB);
	glutInitWindowSize(800, 800);
	glutInitWindowPosition(10, 10);
	glutCreateWindow("Lab 12");

	GLenum glew_status = glewInit();
	if (GLEW_OK != glew_status) {
		std::cout << "Error: " << glewGetErrorString(glew_status) << "\n";   return 1;
	}

	if (!GLEW_VERSION_2_0)
	{
		std::cout << "No support for OpenGL 2.0 found\n";
		return 1;
	}

	initShader();

	init();
	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	glutKeyboardFunc(keyboard);
	glutSpecialFunc(special);
	glutMainLoop();

	freeShader();
}