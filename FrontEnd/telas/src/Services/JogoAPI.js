import { client } from './Client';

export const JogoAPI = {
  async obterTodos() {
    const resposta = await client.get('/jogo');
    return resposta.data;
  },

  async obterPorId(id) {
    const resposta = await client.get(`/jogo/${id}`);
    return resposta.data;
  },

  async cadastrar(jogo) {
    const resposta = await client.post('/jogo', jogo);
    return resposta.data;
  }
};