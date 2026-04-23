import { test, expect } from '@playwright/test';

test.beforeEach(async ({ page }) => {
  await page.goto('/');
});

test.describe('Dashboard', () => {

  test('[WEB] Verificar se o Dashboard exibe Saldo Atual, Receitas e Despesas do Mês', async ({ page }) => {  
    await expect(page.getByText('Saldo Atual')).toBeVisible();
    await expect(page.getByText('Receitas do Mês')).toBeVisible();
    await expect(page.getByText('Despesas do Mês')).toBeVisible();    
  });

  test('[WEB] Verificar se as últimas transações são exibidas na tabela', async ({ page }) => {     
    await expect(page.getByRole('heading',{ name: 'Últimas Transações'})).toBeVisible();
    await expect(page.getByRole('columnheader',{ name: 'Data'})).toBeVisible();
    await expect(page.getByRole('columnheader',{ name: 'Descrição'})).toBeVisible();
    await expect(page.getByRole('columnheader',{ name: 'Categoria'})).toBeVisible();
    await expect(page.getByRole('columnheader',{ name: 'Valor'})).toBeVisible();      
  });

  test('[WEB] Verificar se o menu de navegação contém todos os links', async ({ page }) => {     
    await expect (page.getByRole('listitem').filter({ hasText: 'Dashboard' }).getByRole('link')).toBeVisible();
    await expect (page.getByRole('link',{name: 'Transações'}).nth(1)).toBeVisible();
    await expect (page.getByRole('link',{name: 'Categorias'}).nth(1)).toBeVisible();
    await expect (page.getByRole('link',{name: 'Pessoas'})).toBeVisible();
    await expect (page.getByRole('link',{name: 'Relatórios'}).nth(1)).toBeVisible();
  });

  test('[WEB] Verificar se o link Ver Todas redireciona para Transações', async ({ page }) => {    
    await page.getByRole('link', { name: 'Ver Todas' }).click();
    await expect(page).toHaveURL('/transacoes');    
  });
});