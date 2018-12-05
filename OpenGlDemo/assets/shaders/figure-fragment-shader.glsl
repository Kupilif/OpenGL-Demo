#version 330 core

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct Light {
    vec3 position;

    vec3 ambient;   // should be low, ex. (0.2, 0.2, 0.2)
    vec3 diffuse;   // light color
    vec3 specular;  // usually (1.0, 1.0, 1.0)
};

in vec3 FragmentPosition;  
in vec3 Normal; 

out vec4 color;

uniform Material material;
uniform Light light;
uniform vec3 cameraPosition;

void main()
{
    // Ambient
    vec3 ambient = light.ambient * material.ambient;
  	
    // Diffuse 
    vec3 norm = normalize(Normal);
    vec3 lightDirection = normalize(light.position - FragmentPosition);
    float diff = max(dot(norm, lightDirection), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    // Specular
    vec3 cameraDirection = normalize(cameraPosition - FragmentPosition);
    vec3 reflectDirection = reflect(-lightDirection, norm);  
    float spec = pow(max(dot(cameraDirection, reflectDirection), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    vec3 result = ambient + diffuse + specular;
    color = vec4(result, 1.0f);
}  
