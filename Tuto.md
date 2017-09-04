# Par où commencer ?

Tout d’abord, installez Unity 2017.1 en cliquant sur ce [lien](https://store.unity.com/fr/?_ga=2.109745932.829530078.1503905573-377878170.1501085470).

Puis téléchargez l’archive du projet Road Bands sur sa page [GitHub](https://github.com/ROADBANDS/GAMEJAM)

Lancez Unity, puis cliquez sur le bouton “ OPEN ”. Ouvrez le dossier GAMEJAM que vous trouverez dans l'archive téléchargée.

![alt text](https://user-images.githubusercontent.com/29977168/29970603-86f2dd56-8f25-11e7-8d0d-f31f3558d9fa.png "")


Une fois importée, vous pouvez vérifier que tout s’est bien déroulé en ouvrant la scène Menu.

![alt text](https://user-images.githubusercontent.com/29977168/29970590-7d66d6de-8f25-11e7-923f-97f032bcd6ac.png "")

Cliquez ensuite sur Play pour lancer le jeu.

![alt text](https://user-images.githubusercontent.com/29977168/29970589-7b6fc9b2-8f25-11e7-94ec-94c04607f327.png "")

Si vous le préférez, vous pouvez aussi tester individuellement les différents niveaux du jeu en ouvrant leurs scènes, où même juste vous en inspirer pour concevoir les vôtres !

# Comment créer un nouveau niveau ?

Dans Unity, un niveau s’appelle une scène. Cliquez sur “File” puis “New Scene” pour en créer une (ou faites CTRL+N). Déplacez cette nouvelle scène dans un dossier que vous aurez créé et nommé avec le nom de votre niveau (comme pour BattleOfSandwich, BattleOfGasStation et OutRun). Ce dossier contiendra toutes les ressources que vous aurez créées et dont votre niveau dépendra.

Alternativement, vous pouvez aussi utiliser le dossier Template conçu spécialement pour vous faciliter la vie. Vous n’avez qu’à renommer le dossier et la scène, et remplir les dossiers préexistants avec vos scripts, sons et graphismes !

Un exemple de petit projet qui reprend les assets a été créé dans la scène Template, n’hésitez pas à vous en inspirer !

![alt text](https://user-images.githubusercontent.com/29977168/29970652-aa911d22-8f25-11e7-9d29-fb75fba3edd3.png "")


# Quelles sont les contraintes de game design ?

Vous êtes libres de concevoir vos niveaux comme vous le voulez ! Il n’y a que trois contraintes :

* Votre niveau doit être en deux parties : un échauffement, puis un boss.
* Votre niveau doit être jouable sous l’editor avec le clavier et sur téléphone Android / iPhone.
* À la fin, un score doit être affiché.

Pensez aussi aux débutant(e)s en offrant un petit écran de tutoriel en introduction ;-)

# Est-ce que je peux utiliser des ressources des niveaux déjà existants (assets, sons, graphismes, polices d’écriture) ?

Bien sûr ! N’hésitez pas à détourner ce qui a déjà été fait, pour rester cohérent ou au contraire, pour mieux nous surprendre !

# Est-ce que je peux utiliser des ressources disponibles sur internet (assets, sons, graphismes, polices d’écriture) ? 

Il est possible d’utiliser tous les assets gratuits, accessibles à tout le monde sur internet et dont la licence en permet l’utilisation. Par exemple, tous les assets gratuits sur l’Asset Store de Unity sont utilisables!

# Dois-je intégrer mon niveau au sein du jeu RoadBands ?

Ce n’est pas nécessaire ! Nous nous occuperons d’intégrer votre niveau s’il est sélectionné au sein de la V2 de Roadbands.

# Comment tester mon niveau sur Android ?

Tout d’abord, il vous faut configurer Unity pour Android. Pour cela, suivez les instructions de la [documentation officielle](https://docs.unity3d.com/Manual/android-sdksetup.html).

Placez votre niveau dans la liste des scènes de la fenêtre “Build Settings”, puis choisissez Android comme plateforme. Cliquez sur “Switch Platform”, puis sur Build.

![alt text](https://user-images.githubusercontent.com/29977168/29970596-8028e2d6-8f25-11e7-90d2-a8ce3cccdffe.png "")

Unity va créer un fichier .apk que vous pourrez directement déposer sur votre téléphone à l’aide d’un câble USB. Attention : pour le lancer, vous devez d’abord aller dans les options de votre téléphone, et l’autoriser à exécuter des programmes externes.

# Comment tester mon niveau sur iOS?

Tester le jeu sur votre iPhone est plus compliqué que sur Android : il vous faut un MacOS et un compte développeur Apple. Nous vous conseillons de le tester exclusivement sur Android. Si votre niveau est sélectionné, nous nous occuperons de faire le portage iOS.
