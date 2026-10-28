import { client } from './Client';
import { TokenServico } from './TokenServico';

export const AutenticacaoAPI = {
  async login(email, senha) {
    const dados = typeof email === 'object' ? email : { email, senha };
    
    const resposta = await client.post('/auth/login', dados);
    
    const token = resposta.data?.token || resposta.data?.Token;

    if (token) {
      TokenServico.salvarToken(token);
    }
    
    return resposta.data;
  },

  async registrar(dadosUsuario) {
    const resposta = await client.post('/auth/registrar', dadosUsuario);
    return resposta.data;
  },

  logout() {
    TokenServico.removerToken();
  }
};