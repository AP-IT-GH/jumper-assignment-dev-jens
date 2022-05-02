Eerst maken we een nieuw Unity project aan, we kiezen gewoon 3D als starter project.
Via de package manager installeren we de ML-Agents unity package:
![image](https://user-images.githubusercontent.com/13435783/160552758-033d2e0c-1559-4f4f-a318-6c7c9fe78c41.png)

<br>
<br>

Nu maken we een simpele scene, we hebben een Agent nodig (blauwe cubus in ons geval), een obstakel (rode balk), een platform,
en een item voor bonus punten (groene balk).
Deze items steken we in een leeg gameObject, trainingArea, waar onze agent zal trainen.
![image](https://user-images.githubusercontent.com/13435783/160553873-bcf83c9d-dcb4-4160-8a12-abcbca9fa36f.png)

<br>
<br>

Vervolgens voegen we paar nodige components toe aan de Agent:
![image](https://user-images.githubusercontent.com/13435783/160559624-e773261b-2fb4-403d-aa8b-c4d907adade3.png)
![image](https://user-images.githubusercontent.com/13435783/160559637-23b073c1-3bd7-4534-a32b-306984d64e90.png)
![image](https://user-images.githubusercontent.com/13435783/160559661-6f9346ab-4933-41e0-8a8f-016f7cee94ef.png)

<br>
<br>

Vervolgens maken we een nieuwe script aan voor onze Agent, deze laten we overerven door Agent en overriden we volgende functies:
Ook maken we al een variable (public, we zetten dit in de editor) voor de jumpForce. Ook refereren we naar de rigidbody van de agent, aangezien we hiermee gaan springen.
![image](https://user-images.githubusercontent.com/13435783/160561196-64d14f3c-6876-4539-85e2-59d0b69d3142.png)

<br>
<br>

Nu gaan we de agent laten springen.
Creeer een empty gameobject met de agent als parent, dit is de groundCheck, plaats hem aan de voeten van de agent.
![image](https://user-images.githubusercontent.com/13435783/160573060-3f972a53-d0db-413f-81d9-2f5c8951c216.png)

<br>
<br>

Vervolgens updaten we het Agent script:
We maken variables die we vanuit de editor gaan zetten: LayerMask voor de grond layer, en groundCheck voor het nieuwe aangemaakte groundCheck object van vorige stap.
Ook hebben we een isGrounded variable nodig, deze gaat true zijn als de speler op de grond is, en deze dus mag springen.
We voeren een CheckSphere uit om te checken of de groundCheck object binnen een bepaalde radius de grond raakt, en dus de speler op de grond is.
![image](https://user-images.githubusercontent.com/13435783/160574436-e8cd299f-7fc7-4d18-9d7a-6e1814f83efd.png)

<br>
<br>

Nu passen we OnActionReceived en Heuristic functions aan, zodat we met het keyboard al kunnen testen.
In de OnActionsReceived laten we de agent jumpen als de enige discreteAction op 1 staat, als deze op 0 staat doet de agent niets, ook checken we of de agent zich wel op de grond bevindt.
In de Heuristic zetten we deze enige discrete action op 1 als we op spatie drukken, anders op 0.
![image](https://user-images.githubusercontent.com/13435783/160574742-4db1259a-92b0-48a1-be77-3bbeeebea567.png)

Hier onze values van de AgentScript in de editor
![image](https://user-images.githubusercontent.com/13435783/160577871-ecf3390f-1a2d-4e95-b549-9f6810186a22.png)

In de rigidBody zorgen we dat enkel de y positie niet gelocked is, zo zijn we er zeker van dat de agent niet gaat bewegen naar links of rechts, of gaat roteren zonder we dat willen.
![image](https://user-images.githubusercontent.com/13435783/160577005-2f4318b9-2819-4f6f-914a-4bf52b14845e.png)

<br>
<br>

Volgende wat we gaan doen is het spawnen van de Obstacles en Points.
We maken prefabs van de Obstacles en Points
Vervolgens maken we een nieuw script voor het spawnen, genoemd "Spawner":
In de variabele interval houden we bij om hoeveel seconden we een object willen spawnen.
Als de timer dan op 0 komt, reset hij en spawnt hij een object.
![image](https://user-images.githubusercontent.com/13435783/160581218-811b30b4-91b1-43c8-b1e5-563f4fa39ba8.png)

Op de spawner object zetten we het script, en in de editor slepen we de 2 prefabs in de fields:
![image](https://user-images.githubusercontent.com/13435783/160581515-afc44e8e-9634-42cc-9c6b-b471f5ac6df3.png)

<br>
<br>

Momenteel spawnt de spawner objecten, maar deze blijven gewoon staan, in de volgende stap zullen we ze laten bewegen.
Maak een nieuwe script aan voor de Obstacles en Points, we noemen hem ObjectMover, we zetten dit script ook direct op de prefabs van de obstacles en points.
Eerst maken we een property aan voor de speed, zodat we deze kunnen toekennen wanneer het object gespawned wordt.
In de update method laten we elke frame de objecten in de negatieve z direction bewegen.
Nu moeten we ervoor zorgen dat de objecten despawnen, we doen een if om te checken of de z coordinaat kleiner is dan -10, en dan Destroyen we het gameobject.
![image](https://user-images.githubusercontent.com/13435783/163397499-ff8cf412-e1f0-4339-a439-7cd5f25ee92a.png)

<br>
<br>

In de spawn method van de Spawner klasse zorgen we ervoor dat we de ObjectSpeed van het nieuwe gespawnde object correct setten.
We kennen het toe aan "ObjectSpeed" van de Spawner zelf, deze property gaan we aanpassen bij een nieuwe episode.
![image](https://user-images.githubusercontent.com/13435783/163398107-99008fcd-8a37-4278-b1d6-14e5f4735031.png)
![image](https://user-images.githubusercontent.com/13435783/163398092-8af44bdf-03d5-4983-9f2f-80f24cab357f.png)

<br>
<br>

Nu werkt onze applicatie wel met heuristic, maar moeten we er nog voor zorgen dat de agent zelf kan trainen.
In de OnEpisodeBegin() doen we het volgende: Alle obstacles die nog in de scene zijn destroyen, de speed van de spawner een nieuwe random gegenereerde waarde meegeven.
We maken ook een OnTriggerEnter aan voor te checken of we de obstacles hebben geraakt. We checken nog snel via tag of het een obstacle of een point is, en geven de juiste rewards dan mee.
![image](https://user-images.githubusercontent.com/13435783/163398561-8ca2a182-8939-45f0-9fad-3b440f77c8bc.png)
![image](https://user-images.githubusercontent.com/13435783/163398590-b8454ae2-10fa-42c0-a1cd-8b89a5a50a61.png)

<br>
<br>

### Bugs
Na even trainen heb ik enkele bugs gevonden, eerst en vooral is de ObjectSpeed veel te laag, de agent kan er nooit over springen.
We veranderen dus even de ObjectSpeed die aan de Spawner wordt meegegeven in de OnEpisodeBegin()
![image](https://user-images.githubusercontent.com/13435783/163399627-441c41a7-d88f-4660-81aa-43439e96205e.png)

<br> 

De points worden wel opgeraapt, en er wordt wel rewards gegeven, maar ze blijven nog in de scene nadat de agent deze opraapt.
Om dit te veranderen kunnen we de point gameObject gewoon verwijderen in de OnTrigger method.
![image](https://user-images.githubusercontent.com/13435783/163399945-fee8439f-e11e-4771-8b63-e8f5d940109e.png)

<br>

Wanneer we de agent trainen gaat hij op de duur niet meer sterven, het probleem is dat hij zo gewoon wordt aan de speed die op dat moment gebruikt wordt, dat hij een andere speed niet gaat kunnen.
Als oplossing kunnen we de episode ook beindigen nadat de agent x aantal points heeft verzameld.
![image](https://user-images.githubusercontent.com/13435783/163411545-f864db22-78a1-44af-aff6-6c060d68e3f1.png)
Ook hebben we nog een private variable nodig voor points.
![image](https://user-images.githubusercontent.com/13435783/163411734-0a22846d-5b11-4b9b-b643-fd152fae8f41.png)

<br>

Nu moeten we de agent trainen. Open de anaconda environment waar ml-agents is geinstalleerd, en voer volgende code uit:
```
mlagents-learn Agent.yaml
```
[Link naar Agent.yaml file](AI-Jumper-UNITY/Assets/config/Agent.yaml)
Voor dit voorbeeld heb ik +- 300.000 steps lang getrained, hoe meer hoe accurater de agent natuurlijk.

<br>

Nu dat de agent getrained is, kunnen we het code deel waar de episode stopt na 10 punten, verwijderen:
![image](https://user-images.githubusercontent.com/13435783/166193275-1eb051c8-85dd-4876-b7be-06aaf0d33182.png)

<br>

## Conclusie

Zie hier video van het getrainde model:
https://ap.cloud.panopto.eu/Panopto/Pages/Viewer.aspx?id=2425f69d-1397-4db3-8870-ae89006b12af
Er zijn nog enkele problemen, de AI van de Agent kan niet elke snelheid die de obstacles heeft aan, soms gaan de obstacles te snel, en soms te traag, dit is iets wat misschien na meer steps zou weggaan.

