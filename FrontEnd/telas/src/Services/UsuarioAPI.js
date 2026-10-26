import { client } from './Client';

export const UsuarioAPI = {
    async obterTodos() {
        const resposta = await client.get('/usuario');
        return resposta.data;
    },
    async cadastrar(usuario) {
        const resposta = await client.post('/usuario', usuario);
        return resposta.data;
    }
};