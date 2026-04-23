import { describe, it, expect } from 'vitest'
import { formatCurrency } from '../../../../../ExameDesenvolvedorDeTestes/web/src/lib/formatters'

describe('formatCurrency', () => {
  it('deve formatar valor positivo corretamente', () => {
    const resultado = formatCurrency(100)
    console.log(JSON.stringify(resultado))
    expect(resultado).toContain('100,00')
  })
  
  it('deve formatar valor zero corretamente', () => {
    const resultado = formatCurrency(0)
    console.log(JSON.stringify(resultado))
    expect(resultado).toContain('0,00')
  })

  it('deve formatar valor negativo corretamente', () => {
    const resultado = formatCurrency(-100)
    console.log(JSON.stringify(resultado))
    expect(resultado).toMatch(/-R\$.*100,00/)
    })
})
