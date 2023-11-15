import math
import os


# x tiene que ser el estable
ancho = 2
r = 6.0
theta = 0
num_vertex = 16

lista_puntos_pos = []
lista_puntos_neg =[]

filename = "rodar.obj"
path_of_file = os.path.join(os.path.dirname(__file__), "../Models/", filename)


with open(path_of_file, 'w') as file:
    file.write("# OBJ filen\n")

#puntos

    # Vectores
    file.write("# Vectores\n")

    file.write("v {} {} {} #1\n".format(ancho / 2, 0.0000, 0.0000))
    lista_puntos_pos.append([ancho / 2, 0.0000, 0.0000])

    for i in range(1, num_vertex + 1):
        z = r * math.cos(theta)
        y = r * math.sin(theta)

        z_formatted = float("{:.4f}".format(z))
        y_formatted = float("{:.4f}".format(y))
        file.write("v {} {} {} #{}\n".format(ancho / 2, y_formatted, z_formatted, i + 1))
        theta += 2 * math.pi / num_vertex

        lista_puntos_pos.append([ancho / 2, y_formatted, z_formatted])

    file.write("\n")

    file.write("v {} {} {} #{}\n".format(-1 * (ancho / 2), 0.0000, 0.0000, num_vertex + 2))
    lista_puntos_neg.append([-1*(ancho / 2), 0.0000, 0.0000])

    for i in range(1, num_vertex + 1):
        z = r * math.cos(theta)
        y = r * math.sin(theta)

        z_formatted = float("{:.4f}".format(z))
        y_formatted = float("{:.4f}".format(y))

        file.write("v {} {} {} #{}\n".format(-1*(ancho / 2), y_formatted, z_formatted, i + num_vertex + 2))
        theta += 2 * math.pi / num_vertex

        lista_puntos_neg.append([-1 * (ancho / 2), y_formatted, z_formatted])

    file.write("\n")

    #normalizacion caras
   
    file.write("# Normales\n")

    #B-A
    ba_pos1=lista_puntos_pos[1][0]-lista_puntos_pos[0][0]
    ba_pos2=lista_puntos_pos[1][1]-lista_puntos_pos[0][1]
    ba_pos3=lista_puntos_pos[1][2]-lista_puntos_pos[0][2]


    ba_neg1=lista_puntos_neg[1][0]-lista_puntos_neg[0][0]
    ba_neg2=lista_puntos_neg[1][1]-lista_puntos_neg[0][1]
    ba_neg3=lista_puntos_neg[1][2]-lista_puntos_neg[0][2]


    BA_pos=[ba_pos1,ba_pos2,ba_pos3]
    BA_neg=[ba_neg1,ba_neg2,ba_neg3]

    #C-B
    cb_pos1=lista_puntos_pos[2][0]-lista_puntos_pos[1][0]
    cb_pos2=lista_puntos_pos[2][1]-lista_puntos_pos[1][1]
    cb_pos3=lista_puntos_pos[2][2]-lista_puntos_pos[1][2]

    cb_neg1=lista_puntos_neg[2][0]-lista_puntos_neg[1][0]
    cb_neg2=lista_puntos_neg[2][1]-lista_puntos_neg[1][1]
    cb_neg3=lista_puntos_neg[2][2]-lista_puntos_neg[1][2]

    CB_pos=[cb_pos1,cb_pos2,cb_pos3]
    CB_neg=[cb_neg1,cb_neg2,cb_neg3]

    a=(BA_pos[1]*CB_pos[2])-(BA_pos[2]*CB_pos[1])
    b=(BA_pos[2]*CB_pos[0])-(BA_pos[0]*CB_pos[2])
    c=(BA_pos[0]*CB_pos[1])-(BA_pos[1]*CB_pos[0])

    normal1=[a,b,c]

    t=(BA_neg[1]*CB_neg[2])-(BA_neg[2]*CB_neg[1])
    u=(BA_neg[2]*CB_neg[0])-(BA_neg[0]*CB_neg[2])
    v=(BA_neg[0]*CB_neg[1])-(BA_neg[1]*CB_neg[0])

    normal2=[t,u,v]

    file.write("vn 1.0000 0.000 0.0000 \n")
    file.write("vn -1.0000 0.000 0.0000  \n")

    print(normal1)
    print(normal2) 



#normalización laterales

#Caras


    # Caras
    file.write("\n# Caras\n")

    # Cara 1
    for j in range(num_vertex):
        next_vertex = (j + 1) % num_vertex + 2
        file.write("f 1//1 {}//1 {}//1\n".format(next_vertex, j + 2))

    file.write("\n")

    # Cara 2
    for j in range(num_vertex):
        next_vertex = (j + 1) % num_vertex + num_vertex + 3
        file.write("f {}//2 {}//2 {}//2\n".format(num_vertex + 2, j + num_vertex + 3, next_vertex))


    # Laterales
    file.write("\n# Laterales\n")
    
    for p in range(1, num_vertex):
        file.write("f {} {} {} \n". format(p+2, p+1, p+num_vertex+3))
        file.write("f {} {} {} \n". format(p+num_vertex+3, p+1, p+num_vertex+2))

    dos_1=num_vertex-(num_vertex-1)+1
    ultimo_1= num_vertex+1
    ultimo_2=(num_vertex+1)*2
    dos_2=num_vertex+3
    file.write("f {} {} {} \n". format(dos_1, ultimo_1, dos_2))
    file.write("f {} {} {} \n". format(dos_2, ultimo_1, ultimo_2))