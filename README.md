# Steering Behaviors Allan Oliveira, Carlos Brazil
**Exercício Grau A | Inteligência Artificial para Jogos | Unisinos**


## O que é isso?

Cinco Steering Behaviors clássicos do Craig Reynolds implementados em Unity.
A ideia é simples: você anda por salas com um personagem, e em cada sala tem um NPC
mostrando um comportamento diferente de movimentação autônoma.


## Os Behaviours

### Seek
O NPC mira na sua posição atual e vai direto ao ponto. É o comportamento base para os outros mas é um comportamento burro aonde o NPC apenas vai atras do player e fica sempre atrasado em relaçao a movimentaçao do jogador.

### Flee
O oposto do Seek. Ao jogador entrar em um Raio de panico do NPC ele corre para direçao oposta ao jogador.

### Arrival
Parecido com o Seek, mas com controle. Faz com que o NPC siga o jogador como no seek mas ao chegar a uma certa distancia começa a desacelerar até parar completamente ao chegar no jogador ou alvo (neste caso jogador).

### Wander
O NPC não tem destino. O NPC tem um circulo na sua frente aonde a cada frame ele escolhe uma direçao aleatória nesse circulo e anda fazzendo parecer um comportamento natural

### Pursuit
A versão inteligente do Seek. ao invés de seguir fielmente o jogador e estar sempre atrasado ele preve aonde o jogador vai com e tenta chegar antes do jogador, fazzendo isso através de um fator de prediçao que diz o quão adiantado ele deve ficar em relaçao ao movimento do jogador.

---

## Como jogar

1. Abra a cena `Main` e aperta **Play**
2. Move o personagem com **WASD** ou as **setas do teclado**
3. Chega perto de uma porta e aperta **E** para entrar na sala
4. Para voltar ao Hub, vai até a porta no canto da sala e aperta **E**

Cada sala tem um NPC diferente. Não tem ordem certa — entra em qualquer uma.