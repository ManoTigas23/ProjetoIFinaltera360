import { client } from './Client';
import { TokenServico } from './TokenServico';

export const AutenticacaoAPI = {
    async login(email, senha) {
        const resposta = await client.post('/auth/login', { email, senha });
        if (resposta.data && resposta.data.token) {
            TokenServico.salvarToken(resposta.data.token);
        }
        return resposta.data;
    },
    logout() {
        TokenServico.removerToken();
    }
};