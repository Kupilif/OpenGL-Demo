#version 330 core

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
    float shininess;
};

struct Light {
    int type;

    vec3 position;
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;

    float constant;
    float linear;
    float quadratic;

    float innerCutOff;
    float outerCutOff;
};

out vec4 color;

in vec3 FragmentPosition;  
in vec3 Normal;  

uniform Material material;
uniform Light light;
uniform vec3 cameraPosition;

vec4 ambientLight()
{
    return vec4(light.ambient * material.ambient, 1.0);
}

vec4 calculatePointLight(float intensity)
{
    vec3 ambient = light.ambient * material.ambient;
  	
    vec3 norm = normalize(Normal);
    vec3 lightDirection = normalize(light.position - FragmentPosition);
    float diff = max(dot(norm, lightDirection), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    vec3 cameraDirection = normalize(cameraPosition - FragmentPosition);
    vec3 reflectDirection = reflect(-lightDirection, norm);  
    float spec = pow(max(dot(cameraDirection, reflectDirection), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    float distance = length(light.position - FragmentPosition);
    float attenuation = 1.0 / (light.constant + light.linear * distance + 
    		    light.quadratic * (distance * distance));

    return vec4(ambient * attenuation + diffuse * attenuation * intensity + specular * attenuation * intensity, 1.0);
}

vec4 pointLight()
{
    return calculatePointLight(1.0);
}

vec4 directionalLight()
{
    vec3 ambient = light.ambient * material.ambient;
  	
    vec3 norm = normalize(Normal);
    vec3 lightDirection = normalize(-light.direction);
    float diff = max(dot(norm, lightDirection), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    vec3 cameraDirection = normalize(cameraPosition - FragmentPosition);
    vec3 reflectDirection = reflect(-lightDirection, norm);  
    float spec = pow(max(dot(cameraDirection, reflectDirection), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    return vec4(ambient + diffuse + specular, 1.0);
}

vec4 spotlight()
{   
    float innerCutOffCos = cos(light.innerCutOff);
    float outerCutOffCos = cos(light.outerCutOff);

    vec3 lightDirection = normalize(light.position - FragmentPosition);
    float theta = dot(lightDirection, normalize(-light.direction));

    float epsilon   = innerCutOffCos - outerCutOffCos;
    float intensity = clamp((theta - outerCutOffCos) / epsilon, 0.0, 1.0);

    return calculatePointLight(intensity);
}

void main()
{
    if(light.type == 0)
    {
        color = ambientLight();
        return;
    }

    if(light.type == 1)
    {
        color = directionalLight();
        return;
    }

    if(light.type == 2)
    {
        color = pointLight();
        return;
    }

    if(light.type == 3)
    {
        color = spotlight();
        return;
    }

    color = vec4(1.0);
}
