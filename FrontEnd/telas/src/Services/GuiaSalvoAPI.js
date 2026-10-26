import { client } from './Client';

export const GuiaSalvoAPI = {
    async obterTodos() {
        const resposta = await client.get('/guiasalvo');
        return resposta.data;
    },
    async obterPorId(id) {
        const resposta = await client.get(`/guiasalvo/${id}`);
        return resposta.data;
    },
    async cadastrar(guiaSalvoCriacaoDto) {
        const resposta = await client.post('/guiasalvo', guiaSalvoCriacaoDto);
        return resposta.data;
    },
    async atualizar(id, guiaSalvoCriacaoDto) {
        const resposta = await client.put(`/guiasalvo/${id}`, guiaSalvoCriacaoDto);
        return resposta.data;
    },
    async deletar(id) {
        const resposta = await client.delete(`/guiasalvo/${id}`);
        return resposta.data;
    }
};