# Projet Little Outer Wilds | Antonin Mansour - Zackary Saada

# Introduction (inspiration, idées, etc)

Ce projet a pour but d’appliquer les fondamentaux de l’infographie 3D sur Unity que nous avons pu apprendre pendant le module Infographie 3D - IN11 à l'ESIEE Paris.

Etant fans d’espace, notre projet s’inspire du jeu vidéo Outer Wilds, réalisé également avec Unity. Outer Wilds est un jeu vidéo d'exploration en monde ouvert qui combine des éléments de mystère, d'aventure et de puzzle. Les joueurs incarnent un membre d'une race extraterrestre qui explore un système solaire en constante évolution, découvrant des secrets sur son passé et son destin. Les joueurs explorent des planètes variées, résolvent des énigmes et interagissent avec des civilisations extraterrestres disparues pour comprendre les mystères de l'univers.

![Untitled](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/8f05b791-5e3f-4e1a-a053-1133b3a8552f)

# Description du projet

Notre but est de recréer plusieurs mécaniques clés du jeu de base : Un environnement sans gravité composé de planètes se déplaçant en tant réel, un personnage pouvant se déplacer sur les planètes, et un vaisseau permettant de voyager de planète en planète.

Le gameplay s’en retrouve très simplifié, nous avons préféré mettre l’accent sur les mécaniques de jeu plutôt que sur l’objectif du joueur et ainsi proposer un but simple dans un univers immersif.

Nous avons également fait le choix d’une palette graphique en low-poly pour toutes nos planètes, le vaisseau, et les objets.

# Structure du jeu

## Univers et carte

Le jeux se passe dans un système solaire composé de 5 planètes. une avec un biome désert, une avec un biome tempéré, une avec un biome de type alien, une avec un biome toundra, et enfin une avec un biome banquise. ces 5 planètes viennent de [cet asset](https://assetstore.unity.com/packages/3d/environments/stylized-planet-pack-full-148233).

Pour des raisons de simplicité que l’on détaillera plus dans la section difficultés rencontrés, les planètes sont fixes, c’est la skybox qui tourne sur elle même. Nous avons pu réaliser cet effet en suivant [ce tutoriel](https://youtu.be/ZBf_e4Rr7h4?si=sSbxkKigDbeqsBZA)

Le soleil à été fait à la main en suivant [ce tutoriel](https://www.youtube.com/watch?v=K1BNBZStdxc).

Les décors on été placés a la main, et ils proviennent tous de l’asset store (c.f. la liste associée dans la section source)
![Untitled 1](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/8c5df91c-377f-4746-abcf-3ffa2939041a)

La gravité est gérée grâce à [cet asset](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460). Il est simple d’utilisation. Il faut d’abord désactiver la gravité dans les paramètres du projet. Ensuite, on place le prefab `AttractorSphere` comme enfant de la planète.

Il y a un prefab pour joueur (que l’on détaillera plus dans la section suivante), et un script pour gérer la gravité du vaisseau.

De cette manière, les entités sont maintenant attirés pas le centre de la planète, et non simplement vers le bas comme normalement dans Unity.

![image](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/3f072b5e-0222-4ed7-b525-ab90e94dfc88)


## Joueur

Le joueur provient de l’asset de gravité. Il possède sa propre physique. En effet, il reste forcément sur ces deux pieds, contrairement au vaisseau. De plus il peut marcher, courir, s’accroupir, et sauter.

Nous avons modifié son modèle :

- Changement de la couleur en blanc
- Remplacement de la tête du joueur par un [casque d’astronaute](https://skfb.ly/6WDPY).

Nous avons également changé la caméra du joueur, car elle ne nous convenait pas. Nous avons re-codé un script de contrôle de caméra.

![Untitled 2](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/3b4c9647-d71b-4a11-b051-b3a3dac3e680)

## Vaisseau

Le vaisseau est composé d’un modèle récupéré depuis [cet asset](https://assetstore.unity.com/packages/3d/props/industrial/spaceship-33345) du Unity Store.

Les collisions ont ensuite été appliquées sur le Mesh de ce modèle. Nous avons enrichi le vaisseau avec les fonctionnalités suivantes :

- Plusieurs scripts de contrôle. Ceux-ci permettent le déplacement du vaisseau avec le clavier, l'entrée et sortie du véhicule, et un permettant la réduction de la rotation angulaire pour qu’il soit contrôlable. (l’angular drag built-in des Ridigbody Unity ne répondant pas à nos besoins)
- Une caméra. Celle-ci est fixe derrière le vaisseau et le suit donc dans tous ses déplacements et inclinaisons.
- Des particules de feu tirées de [cet asset](https://assetstore.unity.com/packages/vfx/particles/fire-explosions/procedural-fire-141496) du Unity. Celles-ci sont appelés par un script quand le vaisseau est en train d’être déplacé.
- Des sons qui sont également appelés par un script quand le vaisseau est en train d’être déplacé.
- Une interface utilisateur qui s’affiche quand le joueur est assez proche pour rentrer dans le véhicule.

![Untitled 3](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/cba91f59-56e1-486e-a1d5-4a74dc1b698d)
![Untitled 4](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/c7de4197-57bd-4e11-915e-f20c90d655e3)


Tout ceci permet une conduite immersive dans l’espace, avec un vaisseau respectant des simili-lois de Newton (pas de ralentissement dans l’espace sans frictions ou forces, etc).

https://github.com/Prauwer/LittleOuterWilds/assets/75014657/44cf83cb-43db-4536-b7f5-c87a7687042f


# Fonctionnalités

## Scripts de contrôle

Tous les codes présentés sont des extraits spécifiques et ne représentent pas l’entièreté des scripts utilisés.

### Vaisseau - SpaceshipInteraction.cs

Ce script est important pour ne pas se retrouver à contrôler le vaisseau en même temps que le joueur.

Au lancement du jeu, il désactive le script **SpaceshipController.cs** qui permet au joueur de contrôler le vaisseau. Ensuite, un trigger détecte la présence d’un autre objet à proximité et vérifie qu’il contienne le tag “Player”. Si c’est le cas, l’objet **DriveUI** est activé pour montrer au joueur qu’il peut entrer dans le vaisseau en appuyant sur F.

```csharp
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Candrive = true;
            DriveUI.gameObject.SetActive(true);
        }
        if (driving)
        {
            DriveUI.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Candrive = false;
            DriveUI.gameObject.SetActive(false);
        }
    }
```

Ensuite, on vérifie simplement dans la fonction **Update()** si la possibilité de conduire (booléen *Candrive*) est possible, et si le joueur appuie sur F il entre ou sort du vaisseau. Cela signifie :

- Désactivation de l’objet **Joueur**. Le lier au **Spaceship** pour qu’il reste au même endroit à la sortie du véhicule.
- Activer la caméra du vaisseau et ses contrôles.

Ou inversement en cas de sortie du véhicule.

### Vaisseau - SpaceshipController.cs

Ce script assez complexe gère les éléments de déplacement et rotation du vaisseau, ainsi que ses effets sonores et visuels.

Il effectue plusieurs tâches au début du jeu :

- Récupère un dictionnaire de **touches clavier/directions** pour le contrôle du vaisseau
- **Désactive les particules** de feu
- **Bloque la souris** et la cache (pour un contrôle caméra à la souris)

Le déplacement clavier du vaisseau est géré de cette manière dans la fonction **Update()** :

- Si une touche clavier enfoncée fait partie du dictionnaire défini précédemment, on l’ajoute à un tableau de touches enfoncées
- Cas particulier pour la touche X, qui active la fonction **StopMovement()** pour arrêter le vaisseau
- Sinon, on appelle la fonction **Move(Vector3 direction)** en fonction de la direction de la touche enfoncée. Cette fonction applique une force directionnelle au Rigidbody du vaisseau.
- Ensuite, on appelle **PlaySound(keysPressed)** qui joue un son stereo en fonction des touches et **PlayParticles()** qui allume les feux si besoin

```csharp
void Update()
    {

        bool isKeyPressed = false;
        List<KeyCode> keysPressed = new();

        foreach (var kvp in keyMappings)
        {
            if (Input.GetKey(kvp.Key))
            {
                isKeyPressed = true;

                keysPressed.Add(kvp.Key);

                if (kvp.Key == KeyCode.X)
                {
                    StopMovement();
                }
                else
                {
                    Move(kvp.Value);
                }
            }
            PlaySound(keysPressed);
            PlayParticles();
        }
    } 
```

Le déplacement souris est géré de cette manière :

- On récupère les **entrées souris**.
- Si la touche R est enfoncée, on crée un vecteur torque sur l’**axe (X,Y)** pour le **pitch** et le **roll.**
- Sinon, on crée un vecteur torque sur l’**axe (X,Z)** pour le **pitch** et le **yaw**.
- On applique enfin ce torque au Rigidbody du vaisseau.

```csharp
void Update()
    {

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 torque;
        if (Input.GetKey(KeyCode.R))
        {
            torque = new Vector3(-mouseY, mouseX, 0f) * rotationSpeed;
        }
        else
        {
            torque = new Vector3(-mouseY, 0f, mouseX) * rotationSpeed;
        }

        rb.AddRelativeTorque(torque, ForceMode.Force);
    } 
```

### Vaisseau - ShipRotate.cs

Ce script réduit simplement la vélocité angulaire du Rigidbody du vaisseau d’un certain montant à chaque frame :

```csharp
    void Update()
    {
        rb.angularVelocity *= (1f - angularDragFactor);
    }
```

### Vaisseau - ItemPhysics.cs

Il s’agit d’un script récupéré depuis [l’asset qui nous permet de gérer la gravité](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460) par planète. Ainsi, notre vaisseau est considéré comme un simple objet qui sera attiré par une planète s’il se trouve dans sa sphère d’attraction.

### Joueur - PlayerController.cs

Ce script sert a gérer le ramassage des planètes. Il provient du projet Roll a Ball! des tutoriels Unity. Quand un objet entre en collision avec le joueur, il vérifie si il a le tag “pickup”. Si c’est le cas, désactive l’objet et incrémente le compteur. Ce script gère également l’UI.

```csharp
void OnTriggerEnter(Collider other)
{
    if (other.gameObject.CompareTag("PickUp"))
    {
        other.gameObject.SetActive(false);
        count += 1;
        SetCountText(); 
    }
}

void SetCountText()
{
    countText.text = "Count: " + count.ToString();
    countText.color = Color.red;

    if (count >= 5)
    {
        winTextObject.SetActive(true);
    }
}
```

### Joueur - PlayerPhysics.cs

Il s’agit d’un script récupéré depuis [l’asset qui nous permet de gérer la gravité](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460) par planète. Il gère complètement l’attraction du joueur à sa planète et s’assure qu’il garde bien les pieds sur terre s’il se trouve dans sa sphère d’attraction.

### Joueur - GravityCharacterController.cs

Il s’agit d’un script récupéré depuis [l’asset qui nous permet de gérer la gravité](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460) par planète. Celui-ci gère les animations du joueur, avec quelques modifications pour gérer les changements de gravité.

### Joueur - ThirdPersonUserControls.cs

Il s’agit d’un script récupéré depuis [l’asset qui nous permet de gérer la gravité](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460) par planète. Ce script gère le mapping des touches pour contrôler le joueur.

### Joueur - CamFollowPlayer.cs

Ce script gère la caméra du joueur. Il permet simplement de tourner la caméra de droite à gauche a l’aide de la souris. La caméra est fixée comme enfant du joueur. Nous n’avons pas géré la rotation verticale de la caméra autour du joueur, car ça implique de gérer le fait que le joueur puisse lui aussi pivoter sur cet axe, contrairement à un environnement plat.

```csharp
   // Vitesse de rotation
  public float rotationSpeed = 5.0f;

  // Référence à l'objet joueur
  private Transform parentTransform;

  void Start()
  {
      parentTransform = transform.parent;
  }

  void Update()
  {
      float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;

      parentTransform.Rotate(Vector3.up, mouseX, Space.Self);
  }
```

### Carte - RotateObelisk.cs

Ce script fait simplement tourner un élément de la carte sur lui même d’un certain montant à chaque frame : 

```csharp
void Update()
    {
        transform.Rotate(new Vector3(0, speed, 0));
    }
```

## Effets visuels et sonores

### Soleil

Pour le soleil, nous avons suivi [ce tutoriel](https://www.youtube.com/watch?v=K1BNBZStdxc).

### Vaisseau

Pour ajouter ces effets, il nous a fallu les importer dans notre projet Unity et les insérer dans l’arborescence des éléments concernés (ici, seulement le vaisseau).

On peut ensuite les désactiver et les activer à volonté à l’aide des scripts présentés précédemment.

![Untitled 5](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/bfb59af1-1dda-4447-832e-62dd0ef23c05)

![Untitled 6](https://github.com/Prauwer/LittleOuterWilds/assets/75014657/13ea7b53-6a4c-4114-966b-4c2f3bf294b4)


## Gameplay

Le but du jeu est de se balader sur la carte et de récupérer les mini-planètes. Pour cela, on peut se déplacer à pied sur les planètes et voyager entre elles avec le vaisseau.

Le vaisseau se contrôle de la manière suivante : ZQSD se déplacer à l’horizontale, SPACE et CTRL pour se déplacer verticalement, la souris pour le pan et le tilt, et en appuyant sur R + souris pour le roll. Enfin, on peut appuyer sur R pour freiner dans toute les directions (la rotation freine toute seule).

Le joueur à pied se contrôle de manière assez intuitive, ZQSD pour les déplacements, Shift pour sprinter, C pour s’accroupir, et la souris pour changer l’orientation horizontale de la caméra.

# Difficultés rencontrées

### Contrôles du vaisseau inversés

En codant les déplacements du vaisseau et en appliquant les forces nécessaires pour qu’il se déplace, nous nous sommes rendus compte qu’en passant de l’autre côté d’une planète, les contrôles étaient inversés. Cela s’explique par le fait que nous appliquions des forces relatives au référentiel de la carte plutôt qu’au référentiel du vaisseau.

Il a suffi de créer un vecteur intermédiaire `Vector3 localDirection = transform.TransformDirection(direction);`  pour récupérer le référentiel du vaisseau.

Nous avons également rencontré ce problème lors de l’application d’une force angulaire sur le vaisseau. Utiliser la fonction `addRelativeTorque`  plutôt que `addTorque`  a réglé ce souci.

### Vaisseau qui se bloque dans les colliders

Lors de la première version du vaisseau, on actualisait directement la rotation du vaisseau lors d’un mouvement de souris. Cela causait, par effet de bras de levier, une propulsion non désirée du vaisseau dans l’espace.

Pour régler ce problème, nous avons décidé d’appliquer une force angulaire plutôt que de changer la rotation du vaisseau. Ainsi, la physique est calculée correctement et cela permet d’être plus précis par rapport à l’immersion qu’on souhaite proposer

### Vaisseau qui tourne en boucle

Ce problème est directement issu de la résolution du précédent. Appliquer une force angulaire dans les paramètres de notre monde actuel fait en sorte que le vaisseau va se mettre à tourner indéfiniment sans qu’on lui applique une force inverse. Bien que cela respecte les lois de la physique, au niveau de la jouabilité cela pose un problème.

Pour régler ce problème, nous avons ajouté un script qui stabilise petit à petit la rotation du vaisseau. Cela montre les compromis entre l’immersion qu’on a voulu proposer et le caractère jouable du jeu.

### Planètes qui se déplacent

Initialement, nous voulions faire tourner les planètes autour du Soleil. Chose que nous avons facilement réussi a faire. Cependant, nous avons rencontré un problème : la force d’attraction des planètes étaient trop faible par rapport au joueur, et celui-ci ne suivait pas la planète. Pour gagner du temps, nous avons décidé de faire une illusion. En effet, les planètes sont fixes, c’est la skybox qui tourne sur elle-même et qui donne l’illusion que toutes les planètes bougent.

### Modèle du joueur

Le modèle du joueur ne nous convenait initialement pas, nous trouvions qu’il ne correspondait pas à la direction artistique de notre jeu. Nous avons passé de nombreuses heures à essayer d’importer un autre asset de joueur fonctionnel avec le script de gravité du projet, mais aucun sans succès : on avait soit des problèmes de physique, soit d’animation, ou les deux. N’ayant pas plus de temps à consacrer au joueur, nous avons fait le choix de le laisser tel quel et d’avancer sur le reste des fonctionnalités.

### Caméra 3ème personne

Une caméra à la 3ème personne était fournie avec le scripte gravité. Elle ne nous convenait pas, car on ne pouvait pas pivoter la caméra avec la souris, et elle avait des comportements qui nous semblaient étranges (notamment sur le délai de suivi du personnage). Nous avons donc décidé de re-coder le script de caméra du joueur nous-mêmes.

# Démo

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/50TdcnO13iQ/0.jpg)](https://www.youtube.com/watch?v=50TdcnO13iQ)

# Conclusion

Comme vous avez pu le remarquer, nous avons préféré mettre l’accent sur les mécaniques de jeu plutôt que sur le gameplay, parce que ça permet de rendre le jeu plus facilement évolutif que si on avais fait l’inverse. En effet, maintenant que les mécaniques de jeux sont bien établies, nous pouvons facilement imaginer de nouveaux gameplay (énigmes, histoire, plus de personnages, etc…).

Nous avons beaucoup apprécié travailler sur ce projet étant donné la thématique de l’espace qui nous passionne et nous espérons que vous l’avez apprécié également !

# Code source :

[https://github.com/Prauwer/LittleOuterWilds](https://github.com/Prauwer/LittleOuterWilds)

# Diapositives :

[https://www.canva.com/design/DAGBQaiJ4Sw/QMvljN8Oabl2FGOe4mnBwA](https://www.canva.com/design/DAGBQaiJ4Sw/QMvljN8Oabl2FGOe4mnBwA/edit)

# Sources :

## Assets :

### Planètes :

- [https://assetstore.unity.com/packages/3d/environments/stylized-planet-pack-full-148233](https://assetstore.unity.com/packages/3d/environments/stylized-planet-pack-full-148233)

### Gravité et joueur :

- [https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460](https://assetstore.unity.com/packages/tools/physics/dizzygravity-interplanetary-movement-engine-122460)

### Skybox :

- [https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633](https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633)

### Vaisseau :

- [https://assetstore.unity.com/packages/3d/props/industrial/spaceship-33345](https://assetstore.unity.com/packages/3d/props/industrial/spaceship-33345)
- [https://assetstore.unity.com/packages/vfx/particles/fire-explosions/procedural-fire-141496](https://assetstore.unity.com/packages/vfx/particles/fire-explosions/procedural-fire-141496)

### Décor :

- [https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552](https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552)
- [https://assetstore.unity.com/packages/vfx/shaders/piloto-studio-shaders-258376](https://assetstore.unity.com/packages/vfx/shaders/piloto-studio-shaders-258376)
- [https://assetstore.unity.com/packages/3d/props/low-poly-bird-nests-229812](https://assetstore.unity.com/packages/3d/props/low-poly-bird-nests-229812)
- [https://assetstore.unity.com/packages/3d/environments/fantasy/obelisk-void-221348](https://assetstore.unity.com/packages/3d/environments/fantasy/obelisk-void-221348)
- [https://assetstore.unity.com/packages/3d/environments/landscapes/polydesert-107196](https://assetstore.unity.com/packages/3d/environments/landscapes/polydesert-107196)
- [https://assetstore.unity.com/packages/3d/environments/low-poly-alien-world-132329](https://assetstore.unity.com/packages/3d/environments/low-poly-alien-world-132329)
- [https://assetstore.unity.com/packages/3d/vegetation/trees/snowy-low-poly-trees-76796](https://assetstore.unity.com/packages/3d/vegetation/trees/snowy-low-poly-trees-76796)
- [https://assetstore.unity.com/packages/3d/props/cute-snowman-12477](https://assetstore.unity.com/packages/3d/props/cute-snowman-12477)
- [https://assetstore.unity.com/packages/3d/characters/hand-painted-stones-in-the-snow-62518](https://assetstore.unity.com/packages/3d/characters/hand-painted-stones-in-the-snow-62518)
- [https://assetstore.unity.com/packages/3d/environments/low-poly-vegetation-kit-lite-176906](https://assetstore.unity.com/packages/3d/environments/low-poly-vegetation-kit-lite-176906)
- [https://assetstore.unity.com/packages/3d/environments/fantasy/tent-pack-19370](https://assetstore.unity.com/packages/3d/environments/fantasy/tent-pack-19370)
- [https://assetstore.unity.com/packages/3d/props/interior/wooden-cloth-chair-845](https://assetstore.unity.com/packages/3d/props/interior/wooden-cloth-chair-845)
- [https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552](https://assetstore.unity.com/packages/3d/environments/campfires-torches-models-and-fx-242552)

### Casque :

- [https://skfb.ly/6WDPY](https://skfb.ly/6WDPY)

## Musique :

- Outer Wilds Original Soundtrack : Space - Andrew Prahlow

## Tutoriels :

- [https://youtu.be/K1BNBZStdxc](https://youtu.be/K1BNBZStdxc)
- [https://www.youtube.com/watch?v=ZBf_e4Rr7h4](https://www.youtube.com/watch?v=ZBf_e4Rr7h4)
- ChatGPT
- Forum Unity
