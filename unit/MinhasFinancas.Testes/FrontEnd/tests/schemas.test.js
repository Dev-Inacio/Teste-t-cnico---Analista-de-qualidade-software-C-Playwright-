import { describe, it, expect } from 'vitest'
import { pessoaSchema, categoriaSchema, transacaoSchema } from '../../../../../ExameDesenvolvedorDeTestes/web/src/lib/schemas'

describe('pessoaSchema', () => {
  it('deve rejeitar nome vazio', () => {
    const resultado = pessoaSchema.safeParse({nome:'',dataNascimento: new Date()})
    expect(resultado.success).toBe(false)
    })

    it('deve rejeitar nome com mais de 200 caracteres', () => {
    const resultado = pessoaSchema.safeParse({nome:'a'.repeat(201),dataNascimento: new Date()})
    expect(resultado.success).toBe(false)
    })
})

describe('categoriaSchema', () => {
    it('deve rejeitar descrição vazia', () => {
    const resultado = categoriaSchema.safeParse({ descricao: '', finalidade: 'despesa' })
    expect(resultado.success).toBe(false)
  })  
})

describe('transacaoSchema', () => {
    it('deve rejeitar valor negativo', () => {
    const resultado = transacaoSchema.safeParse({
        descricao: 'teste',
        valor: -100,
        tipo: 'despesa',
        categoriaId: '123',
        pessoaId: '123',
        data: new Date()
    })
    expect(resultado.success).toBe(false)
    })

    it('deve rejeitar valor zero', () => {
    const resultado = transacaoSchema.safeParse({
        descricao: 'teste',
        valor: 0,
        tipo: 'despesa',
        categoriaId: '123',
        pessoaId: '123',
        data: new Date()
    })
    expect(resultado.success).toBe(false)
    })
})
